using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Settings;
using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.BloodSubscription.Service;
using IntegrationLibrary.NewsFromBloodBank.Model;
using IntegrationLibrary.RabbitMQService;
using IntegrationLibrary.Settings;
using IntegrationTest.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Integration
{
    public class BloodSubscriptionTests : BaseIntegrationTest
    {
        public BloodSubscriptionTests(TestDatabaseFactory<Startup> factory) : base(factory){}

        private static MounthlyBloodSubscriptionController SetupController(IServiceScope scope)
        {
            return new MounthlyBloodSubscriptionController(scope.ServiceProvider.GetRequiredService<IBloodSubscriptionService>(), scope.ServiceProvider.GetRequiredService <IBloodBankService>());
        }

        [Fact]
        public void Save_Recived_Message_With_Correct_APIKey_To_Database()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<IntegrationDbContext>().UseNpgsql("Host=localhost;Database=IntegrationDB;Username=postgres;Password=password;").Options;
            var dbContext = new IntegrationDbContext(contextOptions);
            var contextOptions1 = new DbContextOptionsBuilder<HospitalDbContext>().UseNpgsql("Host=localhost;Database=HospitalDB;Username=postgres;Password=password;").Options;
            var hospitalDbContext = new HospitalDbContext(contextOptions1);

            RabbitMQService service = new RabbitMQService();
            var ct = new CancellationTokenSource();

            //act
            RabbitMQPublisher.SendResponseForBloodSubscription();
            service.StartAsync(ct.Token);

            Thread.Sleep(3000);
            ct.Cancel();

            //assert
            List<NewsFromBloodBank> newsList = dbContext.NewsFromBloodBank.ToList();
            NewsFromBloodBank news = newsList.Last();
            Assert.True(news.content == "testna poruka za mjesecnu pretplatu za krv");
            dbContext.NewsFromBloodBank.Remove(news);
            dbContext.SaveChanges();

            List<BloodUnit> buList = hospitalDbContext.BloodUnits.ToList();
            BloodUnit bu = buList.Last();
            Assert.True(bu.BloodType == BloodType.Apos && bu.Amount == 10);
            hospitalDbContext.BloodUnits.Remove(bu);
            hospitalDbContext.SaveChanges();
        }
    }
}
