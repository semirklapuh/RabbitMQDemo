using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")

            };
            using var connection = factory.CreateConnection();
            using var chanell = connection.CreateModel();
            chanell.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(chanell);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            chanell.BasicConsume("demo-queue", true, consumer);

            Console.ReadLine();
        }
    }
}