using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RidingWithRabbit.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();


            channel.QueueDeclare(queue: "clock",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            channel.BasicQos(0, 1, false);

            var reader = new EventingBasicConsumer(channel);
            reader.Received += (m, e) =>
            {
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var remote = Convert.ToInt64(message);
                Console.WriteLine($"Local Time: {DateTime.Now.ToString("HH:mm:ss:fff")}{Environment.NewLine}Remote Time: {DateTime.FromBinary(remote).ToString("HH:mm:ss:fff")}");
            };
            channel.BasicConsume(queue: "clock",
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
