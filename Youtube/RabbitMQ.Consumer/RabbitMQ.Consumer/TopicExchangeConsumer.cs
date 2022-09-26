using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic);
            channel.QueueDeclare("demo-topic-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("demo-topic-queue", "demo-topic-exchange", "account.*"); // topic exchange 可以更好的支持 pattern match. 任何的 account.xxx 都能 match
            channel.BasicQos(0, 10, false); // 允许这个 consumer 去 一次性读取 10 条信息 - 也叫做 prefech 

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-topic-queue", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
