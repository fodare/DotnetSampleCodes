using WindowsServiceExample.Services;

namespace WindowsServiceExample
{
    public class WindowsBackgroundService : BackgroundService
    {
        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly JokeService _jokeService;

        public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger,
            JokeService jokeService)
        {
            _logger = logger;
            _jokeService = jokeService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    string randomJoke = _jokeService.GetJoke();
                    _logger.LogInformation("{joke}", randomJoke);
                    await Task.Delay(3000, stoppingToken);
                }

            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("Exception while getting randonJoke. {exce}, {stackTrace}",
                    ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception while getting random joke. {ex}, {stackTrace}",
                    ex.Message, ex.StackTrace);
                Environment.Exit(1);
            }
        }
    }
}