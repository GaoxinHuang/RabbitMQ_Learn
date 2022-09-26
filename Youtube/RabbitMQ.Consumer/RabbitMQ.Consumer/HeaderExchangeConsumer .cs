using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers);
            channel.QueueDeclare("demo-header-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var header = new Dictionary<string, object>()
            {
                { "account", "new"}
            };

            channel.QueueBind("demo-header-queue", "demo-header-exchange", string.Empty, header); // 就不需要 routing key, 所以是 string.empty, 但是需要 header 
            channel.BasicQos(0, 10, false); // 允许这个 consumer 去 一次性读取 10 条信息 - 也叫做 prefech 

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-header-queue", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
