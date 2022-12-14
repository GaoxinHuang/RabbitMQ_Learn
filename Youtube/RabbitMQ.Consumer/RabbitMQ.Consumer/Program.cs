// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer;
using System.Text;

Console.WriteLine("Hello, World!");
var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
    // user:password@hostname:port
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

#region part 1
//channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
//var consumer = new EventingBasicConsumer(channel);
//consumer.Received += (sender, e) =>
//{
//    var body = e.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    Console.WriteLine(message);
//};
//channel.BasicConsume("demo-queue", true, consumer);
//Console.ReadLine(); 
#endregion

#region part 2
//QueueConsumer.Consume(channel);
//DirectExchangeConsumer.Consume(channel);
#endregion

#region part 3
//TopicExchangeConsumer.Consume(channel);
//HeaderExchangeConsumer.Consume(channel);
FanoutExchangeConsumer.Consume(channel);
#endregion

