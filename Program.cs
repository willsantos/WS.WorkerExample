using Microsoft.EntityFrameworkCore;
using Prometheus;
using Prometheus.Client.DependencyInjection;
using WS.WorkerExample.Application.Interfaces;
using WS.WorkerExample.Application.Services;
using WS.WorkerExample.Data;

namespace WS.WorkerExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            builder.Services
                .AddHostedService<Worker>()
                .AddSingleton<IHttpService, HttpService>()
                .AddSingleton<IMetricFactory>(Metrics.DefaultFactory)
                .AddScoped<IWorkerService, WorkerService>();

            var connectionString = builder.Configuration.GetConnectionString("default");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            
            var host = builder.Build();

            var metricServer = new KestrelMetricServer(port:8080);
            metricServer.Start();
            host.Run();
        }
    }
}