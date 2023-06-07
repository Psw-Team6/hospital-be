using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;


namespace IntegrationLibrary.RabbitMQService.VehicleCoordinate
{
    public class VehicleCoordinateSender:IVehicleCoordinateSender
    {
        public void SendCoordinates(double latitude, double longitude)
        {
            //var factory = new ConnectionFactory { HostName = "localhost" };
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672/")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "vehicle-coordinates",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var coordinates = new
            {
                Latitude = latitude,
                Longitude = longitude
            };

            var json = JsonConvert.SerializeObject(coordinates);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                                 routingKey: "vehicle-coordinates",
                                 basicProperties: null,
                                 body: body);
        }

    }
}
