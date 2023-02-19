using ILoggerExampleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ILoggerExampleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Get WeatherForecast called {time}", DateTime.Now);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "CreateWeather")]
        public async Task<ActionResult<WeatherModel>> CreateWeather(WeatherModel newNotification)
        {
            string notificationData = JsonConvert.SerializeObject(newNotification);
            _logger.LogInformation("New weather notification created at {createTime} with notification data {notificationData}", DateTime.Now, notificationData);
            string newNotificationData = JsonConvert.SerializeObject(newNotification);
            _logger.LogInformation("User created weather notification. Notification data: {notificationData}", newNotificationData);
            return CreatedAtAction("CreateWeather", newNotification);
        }
    }
}