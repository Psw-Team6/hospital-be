using IntegrationLibrary.RabbitMQService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntegrationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ListenForIntegrationEvents();

            CreateHostBuilder(args).Build().Run();
   
        }

        /*private static void ListenForIntegrationEvents()
        {
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

                NewsFromBloodBank data = Newtonsoft.Json.JsonConvert.DeserializeObject<NewsFromBloodBank>(message);
                dbContext.NewsFromBloodBank.Add(data);
                dbContext.SaveChanges();
            };
            channel.BasicConsume(queue: "newsForHospital", autoAck: true, consumer: consumer);
        }*/

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<RabbitMQService>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
