using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.RabbitMQService.RabbitMQProducer
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("mounthlyBloodSubscription", exclusive: false);

            
            var govno = "asdasdasd";
            var json = JsonConvert.SerializeObject(govno);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "mounthlyBloodSubscription", basicProperties: null, body: body);
        }
    }
}
