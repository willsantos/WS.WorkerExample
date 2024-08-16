using System.Data.Common;
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

        public Worker(ILogger<Worker> logger, IHttpService httpService, IHostApplicationLifetime applicationLifetime, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _httpService = httpService;
            _applicationLifetime = applicationLifetime;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            using var scope = _serviceProvider.CreateScope();

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
    }
}