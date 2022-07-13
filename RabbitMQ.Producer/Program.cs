using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ.Producer
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
            var message = new { Name = "Producer", Message = "Hello" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            
            chanell.BasicPublish("","demo-queue", null,body);
        }
    }
}