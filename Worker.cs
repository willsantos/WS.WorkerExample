using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using WS.WorkerExample.Application.Interfaces;
using WS.WorkerExample.Data;
using WS.WorkerExample.Data.Entities;

namespace WS.WorkerExample
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpService _httpService;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMetricFactory _metrics;
        private readonly IHistogram _jobDuration;
        private static readonly Counter LogCounter = Metrics.CreateCounter("log_events_total", "Total number of log events.", labelNames: new[] { "level", "event" });

        public Worker(ILogger<Worker> logger,IMetricFactory metrics, IHttpService httpService, IHostApplicationLifetime applicationLifetime, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _metrics = metrics;
            _httpService = httpService;
            _applicationLifetime = applicationLifetime;
            _serviceProvider = serviceProvider;
            
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            
            var _metricFactory = scope.ServiceProvider.GetRequiredService<IMetricFactory>();
            var _jobDuration = _metricFactory.CreateHistogram("job_duration_seconds", "Duration of job execution in seconds");
            _logger.LogInformation("worker executing db migrations {time}",DateTimeOffset.Now);
            LogCounter.Labels("info", "migration_completed").Inc();
            var stopwatch = Stopwatch.StartNew();

            try
            {
                ApplyMigrations();
            }
            catch (Exception ex)
            {
                _logger.LogError("Falha ao realizar as migrations no banco {Ex}",ex);
                LogCounter.Labels("Error", "task_Failed").Inc();
            }
            finally
            {
                stopwatch.Stop();
                _jobDuration.Observe(stopwatch.Elapsed.TotalSeconds);
            }

            
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            

            var _workerService = scope.ServiceProvider.GetRequiredService<IWorkerService>();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (!await _workerService.HealthCheck())
                {
                    _logger.LogError("Site is down, checked at: {time}", DateTimeOffset.Now);
                    _applicationLifetime.StopApplication();
                }
                else
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var pokemon = await _workerService.GetPokemonApi();

                    if (pokemon != null)
                    {
                        _logger.LogInformation("Pokemon: {name}", pokemon.Name);

                        var funcionario = new Funcionario
                        {
                            Nome = pokemon.Name,
                            CriadoEm = DateTimeOffset.Now
                        };

                        try
                        {
                            if (await _workerService.GetFuncionarioByName(funcionario.Nome, stoppingToken) is not null)
                            {
                                _logger.LogWarning("Pokemon: {name} já cadastrado como funcionario!", pokemon.Name);
                                LogCounter.Labels("info", "funcionario_task").Inc();
                            }
                            else
                            {
                                try
                                {
                                    await _workerService.SaveFuncionario(funcionario, stoppingToken);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, "Error saving Pokemon: {name}", pokemon.Name);
                                    throw;
                                }
                            }
                        }
                        catch (DbException ex)
                        {
                            _logger.LogError(ex, "Error searching Pokemon: {name}", pokemon.Name);
                        }
                    }
                    else
                    {
                        _logger.LogError("Pokemon not found at: {time}, pokemon searched {pkm}", DateTimeOffset.Now, pokemon?.Name);
                    }

                    await Task.Delay(50000, stoppingToken);
                }
            }
        }
        
        private void ApplyMigrations()
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Aplica todas as migrações pendentes
            dbContext.Database.Migrate();
        }
    }
}