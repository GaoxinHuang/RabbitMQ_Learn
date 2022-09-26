using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class HeaderExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 } // 生命周期 30 秒
            };
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers, arguments: ttl);
            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "new"} }; // 这个header 比较 安全match 

                channel.BasicPublish("demo-header-exchange", String.Empty, properties, body); // 对于 header exchange, 需要传入 property 里面带 header 
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
