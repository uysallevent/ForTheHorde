using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RidingWithRabbit.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();


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

            WriteLineWait("Press [enter] to exit.");
        }

        public static void WriteLineWait(string value)
        {
            Console.WriteLine(value);
            Console.ReadLine();
        }
    }
}
