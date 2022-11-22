using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationLibrary.NewsFromBloodBank.Model;
using IntegrationLibrary.NewsFromBloodBank.Service;
using IntegrationLibrary.RabbitMQService;
using IntegrationLibrary.Settings;
using IntegrationTest.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Integration
{
    public class NewsFromBloodBankTests : BaseIntegrationTest
    {
        public NewsFromBloodBankTests(TestDatabaseFactory<Startup> factory) : base(factory){}

        private static NewsFromBloodBankController SetupController(IServiceScope scope) 
        {
            return new NewsFromBloodBankController(scope.ServiceProvider.GetRequiredService<INewsFromBloodBankService>());   
        }

        [Fact]
        public void Update_NewsFromBloodBank_Successfuly()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewsFromBloodBank news = controller.GetFirst();
            news.newsStatus = IntegrationLibrary.Enums.NewsFromHospitalStatus.ACTIVE;

            var result = controller.Update(news.Id, news);

            result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public void Update_NewsFromBloodBank_Unsuccessfuly()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewsFromBloodBank news = new()
            {
                Id = Guid.NewGuid(),
                title = "Dobrovoljno davanje krvi",
                content = "Dodjite na dobrovoljno davanje krvi sutra.",
                apiKey = "NkwQR/sa7Rm97+S7/KQxqWl2nZhnWjzLX3dvHOTngEk=",
                newsStatus = IntegrationLibrary.Enums.NewsFromHospitalStatus.ON_HOLD,
                base64image = "",
                bloodBankName = "Vampir"
            };

            var result = controller.Update(Guid.NewGuid(), news);

            result.ShouldBeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Save_Recived_Message_With_Correct_APIKey_To_Database()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<IntegrationDbContext>().UseNpgsql("Host=localhost;Database=IntegrationDB;Username=postgres;Password=password;").Options;
            var dbContext = new IntegrationDbContext(contextOptions);
            RabbitMQService service = new RabbitMQService();
            var ct = new CancellationTokenSource();

            //act
            RabbitMQPublisher.SendWithCorrectAPIKey();
            service.StartAsync(ct.Token);

            Thread.Sleep(3000);
            ct.Cancel();

            //assert
            List<NewsFromBloodBank> newsList = dbContext.NewsFromBloodBank.ToList();
            Assert.True(newsList.Last().title == "Testni naslov");
            dbContext.NewsFromBloodBank.Remove(newsList.Last());
            dbContext.SaveChanges();
        }

        [Fact]
        public void Refuse_Recived_Message_With_Incorrect_APIKey()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<IntegrationDbContext>().UseNpgsql("Host=localhost;Database=IntegrationDB;Username=postgres;Password=password;").Options;
            var dbContext = new IntegrationDbContext(contextOptions);
            RabbitMQService service = new RabbitMQService();
            var ct = new CancellationTokenSource();

            //act
            RabbitMQPublisher.SendWithIncorrectAPIKey();
            service.StartAsync(ct.Token);

            Thread.Sleep(3000);
            ct.Cancel();

            //assert
            List<NewsFromBloodBank> newsList = dbContext.NewsFromBloodBank.ToList();
            Assert.True(newsList.Last().title != "Testni naslov");
            dbContext.NewsFromBloodBank.Remove(newsList.Last());
            dbContext.SaveChanges();
        }

    }
}
