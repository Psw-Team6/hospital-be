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

namespace IntegrationLibrary.RabbitMQService
{
    public class RabbitMQService : BackgroundService
    {
        IConnection connection;
        IModel channel;
        public RabbitMQService() { }
        public override Task StartAsync(CancellationToken cancellationToken)
        {/*
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            var contextOptions = new DbContextOptionsBuilder<IntegrationDbContext>().UseNpgsql("Host=localhost;Database=IntegrationDB;Username=postgres;Password=password;").Options;
            var dbContext = new IntegrationDbContext(contextOptions);

            consumer.Received += (model, ea) =>
            {

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                NewsFromBloodBank.Model.NewsFromBloodBank data = Newtonsoft.Json.JsonConvert.DeserializeObject<NewsFromBloodBank.Model.NewsFromBloodBank>(message);
                List<BloodBank.BloodBank> bbList = dbContext.BloodBanks.ToList();
                foreach (BloodBank.BloodBank bb in bbList) {
                    if (data.apiKey.Equals(bb.ApiKey)) {
                        dbContext.NewsFromBloodBank.Add(data);
                        dbContext.SaveChanges();
                    }
                }
            };
            channel.BasicConsume(queue: "newsForHospital", autoAck: true, consumer: consumer);*/
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {/*
            channel.Close();
            connection.Close();*/
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
