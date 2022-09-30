using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 } // 生命周期 30 秒
            };
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct, arguments: ttl);
            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("demo-direct-exchange", "account.update", null, body); // 第二个参数是 routing key
                //channel.BasicPublish("demo-direct-exchange", "user.update", null, body); // 改成 user.update 则 exchange 上有message, 但是 queue 就不会生成任何message
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
