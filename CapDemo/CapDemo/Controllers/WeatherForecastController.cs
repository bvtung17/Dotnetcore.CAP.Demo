using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace CapDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICapPublisher _capPublisher;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICapPublisher capPublisher)
        {
            _logger = logger;
            _capPublisher = capPublisher;
        }

        [HttpPost("helloMethod")]
        public async Task<IActionResult> CallHelloMethod(string message)
        {
            using (var connection = new MySqlConnection("server=localhost;Port=3306;user=root;password=password123;database=Demo;ConnectionTimeout=120;"))
            {
                using var transaction = connection.BeginTransaction(_capPublisher);
                {
                    await _capPublisher.PublishAsync("helloMethod", message);
                    var delay = TimeSpan.FromSeconds(10);
                    await _capPublisher.PublishDelayAsync(delay, "helloMethodRabbitMQ", $"{message} + RabbitMQ");
                    await transaction.CommitAsync();
                }
            }

            return Ok();
        }
    }
}