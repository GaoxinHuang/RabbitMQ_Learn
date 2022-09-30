// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Producer;
using System.Text;

Console.WriteLine("Hello, World!");
var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
    // user:password@hostname:port
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

#region Part 1
//channel.QueueDeclare("demo-queue", durable:true, exclusive:false, autoDelete:false, arguments:null);
//var message = new { Name = "Producer", Message = "Hello!" };
//var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
//channel.BasicPublish("", "demo-queue", null, body); 
#endregion

#region Part 2
//QueueProducer.Publish(channel); 
//DirectExchangePublisher.Publish(channel);
#endregion

#region Part 3
//TopicExchangePublisher.Publish(channel);
//HeaderExchangePublisher.Publish(channel);
FanoutExchangePublisher.Publish(channel);
#endregion
