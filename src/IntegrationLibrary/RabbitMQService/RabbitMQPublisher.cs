using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationLibrary.NewsFromBloodBank.Model;
using Newtonsoft.Json;
using IntegrationLibrary.RabbitMQPublisher;
using IntegrationLibrary.BloodSubscription.Model;

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


                NewsFromBloodBank.Model.NewsFromBloodBank news = new(Guid.NewGuid(), "Testni naslov", "Testni kontent", "NkwQR/sa7Rm97+S7/KQxqWl2nZhnWjzLX3dvHOTngEk=",
                                                                    Enums.NewsFromHospitalStatus.ON_HOLD, "", "Vampir");

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


                NewsFromBloodBank.Model.NewsFromBloodBank news = new(Guid.NewGuid(), "Testni naslov", "Testni kontent", "Nevalidan API Key",
                                                                Enums.NewsFromHospitalStatus.ON_HOLD, "", "Vampir");

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(news));

                channel.BasicPublish(exchange: "",
                                     routingKey: "newsForHospital",
                                     basicProperties: null,
                                     body: body);
            }
        }

        public static void SendBloodSubscription(MounthlyBloodSubscriptionResponse bSub)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bSub));

                channel.BasicPublish(exchange: "",
                                     routingKey: "bloodSubscription",
                                     basicProperties: null,
                                     body: body);
            }
        }

        public static void SendResponseForBloodSubscription()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                List<AmountOfBloodType> list = new List<AmountOfBloodType>();

                AmountOfBloodType aobt1 = new(Enums.BloodType.Apos, 10);

                list.Add(aobt1);

                MounthlyBloodSubscriptionDTO bloodSubDTO = new()
                {
                    APIKey = "gcbrXSsoer1L5TqFMbgU+LFWV2G3VaciUeG9YOtS9FM=",
                    bloodTypeAmountPair = list,
                    messageForManager = "testna poruka za mjesecnu pretplatu za krv"
                };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bloodSubDTO));

                channel.BasicPublish(exchange: "",
                                     routingKey: "newsForHospital",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
