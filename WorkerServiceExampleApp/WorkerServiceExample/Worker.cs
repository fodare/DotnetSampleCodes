namespace WorkerServiceExample
{
    public class Worker : BackgroundService
    {
        Random newNumber = new Random();
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public Worker(ILogger<Worker> logger, IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _applicationLifetime = applicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting application at: {time}", DateTime.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                LogRandomNumber();
                /*_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);*/
                await Task.Delay(3000, stoppingToken);

                // Stops application when Task is completed
                _logger.LogInformation("Stopping application at: {time}", DateTime.Now);
                _applicationLifetime.StopApplication();
               
            }
        }

        private void LogRandomNumber()
        {
            var num = newNumber.Next(1, 101);
            _logger.LogInformation("Generated random number. Number is: {num}", num);
        }
    }
}