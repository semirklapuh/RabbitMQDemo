using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ.Producer
{
    public class QueueProducer
    {
        public static void Publish(IModel chanell)
        {
            chanell.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                arguments: null);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                chanell.BasicPublish("","demo-queue", null,body);
                count ++;
                Thread.Sleep(1000);
            }
        }
    }
}