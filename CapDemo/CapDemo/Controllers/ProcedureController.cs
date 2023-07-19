using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace CapDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcedureController : ControllerBase
    {
        private readonly ICapPublisher _capPublisher;
        public ProcedureController(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        [HttpPost("helloMethod")]
        public async Task<IActionResult> CallHelloMethod(string message)
        {
            //using (var connection = new MySqlConnection("server=localhost;Port=3306;user=root;password=password123;database=Demo;ConnectionTimeout=120;"))
            //{
            //    using var transaction = connection.BeginTransaction(_capPublisher);
            //    {
            await _capPublisher.PublishAsync("helloMethodRequest", $"{message} + RabbitMQ");
            //await transaction.CommitAsync();
            //    }
            //}

            return Ok();
        }
    }
}