using Microsoft.EntityFrameworkCore;
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
                .AddScoped<IWorkerService, WorkerService>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=localhost,1433;Database=Worker-dev;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");
            });

            var host = builder.Build();
            host.Run();
        }
    }
}