using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationLibrary.NewsFromBloodBank.Model;
using Newtonsoft.Json;

namespace IntegrationLibrary.RabbitMQService
{
    public class RabbitMQPublisher
    {
        public static void SendWithCorrectAPIKey()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                

                NewsFromBloodBank.Model.NewsFromBloodBank news = new()
                {
                    Id = Guid.NewGuid(),
                    title = "Testni naslov",
                    content = "Testni kontent",
                    apiKey = "NkwQR/sa7Rm97+S7/KQxqWl2nZhnWjzLX3dvHOTngEk=",
                    newsStatus = Enums.NewsFromHospitalStatus.ON_HOLD,
                    base64image = "",
                    bloodBankName = "Vampir"
                };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(news));

                channel.BasicPublish(exchange: "",
                                     routingKey: "newsForHospital",
                                     basicProperties: null,
                                     body: body);
            }
        }

        public static void SendWithIncorrectAPIKey()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {


                NewsFromBloodBank.Model.NewsFromBloodBank news = new()
                {
                    Id = Guid.NewGuid(),
                    title = "Testni naslov",
                    content = "Testni kontent",
                    apiKey = "Nevalidan API Key",
                    newsStatus = Enums.NewsFromHospitalStatus.ON_HOLD,
                    base64image = "",
                    bloodBankName = "Vampir"
                };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(news));

                channel.BasicPublish(exchange: "",
                                     routingKey: "newsForHospital",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
