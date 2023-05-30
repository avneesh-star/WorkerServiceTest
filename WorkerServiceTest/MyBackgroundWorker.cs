using WorkerServiceTest.Services;

namespace WorkerServiceTest
{
    public class MyBackgroundWorker : BackgroundService
    {
        private readonly ILogger<MyBackgroundWorker> _logger;
        private readonly IServiceScopeFactory _factory;
        private readonly IConfiguration _configuration;
        private readonly TimeSpan _period = TimeSpan.FromMinutes(1);

        public MyBackgroundWorker(ILogger<MyBackgroundWorker> logger,
            IServiceScopeFactory factory,
            IConfiguration configuration)
        {
            _logger = logger;
            _factory = factory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
          
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                    IDemoService demoService = asyncScope.ServiceProvider.GetRequiredService<IDemoService>();
                    demoService.DoSomething();
                    _logger.LogInformation($"MyBackgroundWorker is running. at {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
                }
            }

        }
    }
}
