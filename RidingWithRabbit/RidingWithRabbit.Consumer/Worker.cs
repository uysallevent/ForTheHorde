using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RidingWithRabbit.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();



            //while (!stoppingToken.IsCancellationRequested)
            //{
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                channel.QueueDeclare(queue: "commands",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var reader = new EventingBasicConsumer(channel);
                reader.Received += (m, e) =>
                {
                    var body = e.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine($"{message}");
                    Thread.Sleep(500);
                };
                channel.BasicConsume(queue: "commands",
                                     autoAck: true,
                                     consumer: reader);

                Console.WriteLine("All messages has been read");

                await Task.Delay(1000, stoppingToken);
            //}


        }
        
        private void RabbitConsumer()
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

                    var reader = new EventingBasicConsumer(channel);
                    reader.Received += (m, e) =>
                    {
                        var body = e.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine($"{message}");
                        Thread.Sleep(500);
                    };
                    channel.BasicConsume(queue: "commands",
                                         autoAck: true,
                                         consumer: reader);

                    Console.WriteLine("Teşekkürler Necati Amca :)");
                    Console.ReadLine();
                }
            }
        }
    }
}
