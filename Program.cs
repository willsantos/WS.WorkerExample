using Microsoft.EntityFrameworkCore;
using Prometheus;
using Prometheus.Client.DependencyInjection;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using WS.WorkerExample.Application.Interfaces;
using WS.WorkerExample.Application.Services;
using WS.WorkerExample.Data;

namespace WS.WorkerExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.GrafanaLoki("http://loki:3100", new []
                {
                    new LokiLabel
                    {
                        Key = "app",
                        Value = "workerExample"
                    }
                })
                .CreateLogger();

            try
            {
                var builder = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((hostContext, services) =>
                    {
                        services
                            .AddHostedService<Worker>()
                            .AddSingleton<IHttpService, HttpService>()
                            .AddSingleton<IMetricFactory>(Metrics.DefaultFactory)
                            .AddScoped<IWorkerService, WorkerService>();

                        var connectionString = hostContext.Configuration.GetConnectionString("default");
                        services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(connectionString); });
                    })
                    .UseSerilog(); 
                var host = builder.Build();

                var metricServer = new KestrelMetricServer(port: 8080);
                metricServer.Start();
                host.Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}