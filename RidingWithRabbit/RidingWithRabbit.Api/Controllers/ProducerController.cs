using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RidingWithRabbit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private EventHandler<string> _eventHandler;
        private System.Timers.Timer _timer;
        private IModel _channel;

        [HttpGet]
        public IActionResult SendMessageToQue()
        {
            try
            {
                var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
                var connection = connectionFactory.CreateConnection();
                var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "clock",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                _channel = channel;
                IntervalPublish(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Opppps. Exception => {Environment.NewLine} {JsonConvert.SerializeObject(ex)}");
            }
            return Ok("Messages has sent to que");
        }


        private void IntervalPublish(int interval)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;
            var message = now.ToBinary().ToString();
            WriteLinePos(now.ToString(), 0, 10);
            Basic("clock",message);
            Console.WriteLine($"'{message}' has sent");
        }

        private void Basic(string routingKey,string message)
        {
            _channel.BasicPublish(exchange: "",
                     routingKey: routingKey,
                     basicProperties: null,
                     body: Encoding.UTF8.GetBytes(message));
        }

        private static void WriteLinePos(string value, int left = 0, int top = 0)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine(value);
        }
    }
}
