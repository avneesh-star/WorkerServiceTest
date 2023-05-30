using WorkerServiceTest.Data;
using WorkerServiceTest.Services;

namespace WorkerServiceTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<MyBackgroundWorker>();
                services.AddScoped<IAppDbContext, AppDbContext>();
                services.AddScoped<IDemoService, DemoService>();
            })
            .Build();
            await host.RunAsync();
        }
    }
}