using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IntegrationLibrary.NewsFromBloodBank.Model;
using IntegrationLibrary.RabbitMQPublisher;
using System.Net.Http;
using IntegrationLibrary.PDFReports.Model;
using System.Net.Http.Json;
using IntegrationLibrary.BloodSubscription.Model;

namespace IntegrationLibrary.RabbitMQService
{
    public class RabbitMQService : BackgroundService
    {
        IConnection connection;
        IModel channel;
        public RabbitMQService() { }
        public override Task StartAsync(CancellationToken cancellationToken)
        {

            reciveNewsFromBloodBank(cancellationToken);

            //reciveResponseForBloodSubscription(cancellationToken);

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            channel.Close();
            connection.Close();
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        private void reciveNewsFromBloodBank(CancellationToken cancellationToken)
        {
            try
            {
                var factory = new ConnectionFactory();
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                var consumer = new EventingBasicConsumer(channel);

                var contextOptions = new DbContextOptionsBuilder<IntegrationDbContext>().UseNpgsql("Host=localhost;Database=IntegrationDB;Username=postgres;Password=tamara;").Options;
                var dbContext = new IntegrationDbContext(contextOptions);

                consumer.Received += (model, ea) =>
                {

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    MounthlyBloodSubscriptionResponse bs = Newtonsoft.Json.JsonConvert.DeserializeObject<MounthlyBloodSubscriptionResponse>(message);
                    NewsFromBloodBank.Model.NewsFromBloodBank data = Newtonsoft.Json.JsonConvert.DeserializeObject<NewsFromBloodBank.Model.NewsFromBloodBank>(message);
                    MounthlyBloodSubscriptionDTO data1 = Newtonsoft.Json.JsonConvert.DeserializeObject<MounthlyBloodSubscriptionDTO>(message);


                    //hvata slucaj kada saljem odgovor na mjesecnu pretplatu za krv
                    if (data1.APIKey != null && data1.bloodTypeAmountPair != null && data1.messageForManager != null)
                    {
                        List<BloodBank.BloodBank> bbList1 = dbContext.BloodBanks.ToList();
                        foreach (BloodBank.BloodBank bb in bbList1)
                        {
                            if (data1.APIKey.Equals(bb.ApiKey.Value))
                            {
                                HttpClient httpClient = new HttpClient();
                                //BloodBank.BloodBank bloodBank = httpClient.GetFromJsonAsync<BloodBank.BloodBank>("http://localhost:5001/api/BloodBank/findByAPIKey/" + data.APIKey).Result;
                                List<BloodBank.BloodBank> bloodBanks = dbContext.BloodBanks.ToList();
                                BloodBank.BloodBank bloodBank = getByApiKey(data1.APIKey, bloodBanks);
                                //List<BloodUnit> bloodUnits = getByBloodBankName(bloodBank.Name, httpClient);
                                foreach (AmountOfBloodType aobt in data1.bloodTypeAmountPair)
                                {
                                    BloodUnit bloodUnit = new BloodUnit();
                                    bloodUnit.BloodType = (IntegrationLibrary.PDFReports.Model.BloodType)Enum.Parse(typeof(BloodType), aobt.bloodType.ToString());
                                    bloodUnit.Amount = aobt.amount;
                                    bloodUnit.Consumptions = null;
                                    bloodUnit.BloodBankName = bloodBank.Name;
                                    if (bloodUnit.Amount > 0)
                                    {
                                        httpClient.PostAsJsonAsync<BloodUnit>("http://localhost:5000/api/v1/BloodUnit", bloodUnit);
                                    }
                                }

                                NewsFromBloodBank.Model.NewsFromBloodBank news = new NewsFromBloodBank.Model.NewsFromBloodBank("Mjesecna isporuka krvi", data1.messageForManager,
                                                                                        data1.APIKey, Enums.NewsFromHospitalStatus.BLOOD_SUBSCRIPTION, "", bloodBank.Name);

                                dbContext.NewsFromBloodBank.Add(news);
                                dbContext.SaveChanges();
                            }
                        }
                    }
                    else if (data.content == null) 
                    {
                        return;
                    }
                    else
                    {
                        List<BloodBank.BloodBank> bbList = dbContext.BloodBanks.ToList();
                        foreach (BloodBank.BloodBank bb in bbList)
                        {
                            if (data.apiKey.Equals(bb.ApiKey.Value))
                            {
                                dbContext.NewsFromBloodBank.Add(data);
                                dbContext.SaveChanges();
                            }
                        }
                    }                };
                channel.BasicConsume(queue: "newsForHospital", autoAck: true, consumer: consumer);
            }
            catch (Exception e) {
                
            }
            
        }

        /*private void reciveResponseForBloodSubscription(CancellationToken cancellationToken)
        {
            try
            {
                var factory = new ConnectionFactory();
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                var consumer = new EventingBasicConsumer(channel);

                var contextOptionsIntegration = new DbContextOptionsBuilder<IntegrationDbContext>().UseNpgsql("Host=localhost;Database=IntegrationDB;Username=postgres;Password=password;").Options;
                var dbContextIntegration = new IntegrationDbContext(contextOptionsIntegration);

                consumer.Received += (model, ea) =>
                {

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    MounthlyBloodSubscriptionDTO data = Newtonsoft.Json.JsonConvert.DeserializeObject<MounthlyBloodSubscriptionDTO>(message);
                    List<BloodBank.BloodBank> bbList = dbContextIntegration.BloodBanks.ToList();
                    foreach (BloodBank.BloodBank bb in bbList)
                    {
                        if (data.APIKey.Equals(bb.ApiKey))
                        {
                            HttpClient httpClient = new HttpClient();
                            //BloodBank.BloodBank bloodBank = httpClient.GetFromJsonAsync<BloodBank.BloodBank>("http://localhost:5001/api/BloodBank/findByAPIKey/" + data.APIKey).Result;
                            List<BloodBank.BloodBank> bloodBanks = dbContextIntegration.BloodBanks.ToList();
                            BloodBank.BloodBank bloodBank = getByApiKey(data.APIKey, bloodBanks);
                            //List<BloodUnit> bloodUnits = getByBloodBankName(bloodBank.Name, httpClient);
                            foreach (AmountOfBloodType aobt in data.bloodTypeAmountPair)
                            {
                                BloodUnit bloodUnit = new BloodUnit();
                                bloodUnit.BloodType = (IntegrationLibrary.PDFReports.Model.BloodType)Enum.Parse(typeof(BloodType), aobt.bloodType.ToString());
                                bloodUnit.Amount = aobt.amount;
                                bloodUnit.Consumptions = null;
                                bloodUnit.BloodBankName = bloodBank.Name;
                                if (bloodUnit.Amount > 0)
                                {
                                    httpClient.PostAsJsonAsync<BloodUnit>("http://localhost:5000/api/v1/BloodUnit", bloodUnit);
                                }
                            }

                            NewsFromBloodBank.Model.NewsFromBloodBank news = new NewsFromBloodBank.Model.NewsFromBloodBank();
                            news.title = "Mjesecna isporuka krvi";
                            news.content = data.messageForManager;
                            news.apiKey = data.APIKey;
                            news.base64image = "";
                            news.newsStatus = Enums.NewsFromHospitalStatus.BLOOD_SUBSCRIPTION;
                            news.bloodBankName = bloodBank.Name;

                            dbContextIntegration.NewsFromBloodBank.Add(news);
                            dbContextIntegration.SaveChanges();
                        }
                    }
                };
                channel.BasicConsume(queue: "responseBloodSubscription", autoAck: true, consumer: consumer);
            }
            catch (Exception e) 
            {  
            }
        }*/

        private BloodBank.BloodBank getByApiKey(String apikey, List<BloodBank.BloodBank> bloodBanks) 
        {
            
            foreach (BloodBank.BloodBank bb in bloodBanks) 
            {
                if (bb.ApiKey.Value.Equals(apikey))
                    return bb;
            }
            return null; 
        }
    }
}
