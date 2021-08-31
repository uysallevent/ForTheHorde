using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RidingWithRabbit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        [HttpGet]
        public IActionResult SendMessageToQue()
        {
            try
            {
                var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "commands",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        var messages = new List<string>{
                                "Riding With The king",
                                "Key To The HighWay",
                                "Marry You",
                                "Three O'Clock Blues"
                            };

                        foreach (var message in messages)
                        {
                            var body = Encoding.UTF8.GetBytes(message);
                            channel.BasicPublish(exchange: "",
                                             routingKey: "commands",
                                             basicProperties: null,
                                             body: body);
                            Console.WriteLine($"'{message}' has sent");
                            Task.Delay(800);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Opppps. Exception => {Environment.NewLine} {JsonConvert.SerializeObject(ex)}");
            }
            return Ok("Messages has sent to que");
        }
    }
}
