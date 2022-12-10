using AutoMapper;
using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPI.Mapper;
using IntegrationAPI.ScheduleTask.Service;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;
using IntegrationLibrary.ConfigureGenerateAndSend.Service;
using IntegrationTest.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Integration
{
    public class ConfigureGenerateAndSendTest: BaseIntegrationTest
    {
        public ConfigureGenerateAndSendTest(TestDatabaseFactory<Startup> factory) : base(factory) { }
        private IMapper _maper= new Mapper(new MapperProvider().GetMapperConfig());


        private static ConfigureGenerateAndSendController SetupController(IServiceScope scope, IMapper maper)
        {
            return new ConfigureGenerateAndSendController(scope.ServiceProvider.GetRequiredService<IConfigureGenerateAndSendService>(),  maper);
        }


        /*
        [Fact]
        public void Edit_configuration_sucessfully()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, _maper);
            ConfigureGenerateAndSend request = controller.GetFirst();
            request.SendPeriod = "SIX_MONTH";
            request.NextDateForSending = DateTime.Now;

            var result = controller.Edit(request);

            result.ShouldBeOfType<OkObjectResult>();
        }
        */


        /*[Fact]
        public void Edit_configuration_unsucessfully_configuration_is_null()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, _maper);
            ConfigureGenerateAndSend request = null;

            var result = controller.Edit(request);

            result.ShouldBeOfType<BadRequestResult>();
        }*/


        /*[Fact]
        public void Edit_configuration_unsucessfully_some_field_is_null()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, _maper);
            ConfigureGenerateAndSend request = controller.GetFirst();
            request.BloodBankName = null;

            var result = controller.Edit(request);

            result.ShouldBeOfType<BadRequestResult>();
        }*/


        /*
        [Fact]
        public void Create_configuration_sucessfully()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, _maper);
            ConfigureGenerateAndSend configuration = new()
            {
                Id = Guid.NewGuid(),
                BloodBankName = "Nova banka-otvoreno",
                GeneratePeriod = "TWO_MONTH",
                SendPeriod = "SIX_MONTH",
                NextDateForSending = DateTime.Now,
            };

            var result = controller.Create(configuration);

            result.ShouldBeOfType<OkObjectResult>();
        }


        [Fact]
        public void Create_configuration_unsucessfully_is_null()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, _maper);
            ConfigureGenerateAndSend configuration = null;

            var result = controller.Create(configuration);

            result.ShouldBeOfType<BadRequestResult>();
        }

        [Fact]
        public void Create_configuration_unsucessfully_already_exist()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, _maper);
            ConfigureGenerateAndSend configuration = controller.GetFirst();

            var result = controller.Create(configuration);

            result.ShouldBeOfType<BadRequestResult>();
        }
        */



        [Fact]
        public void Next_send_period_calculate_corect()
        {
            CalculateDate calculateDate = new CalculateDate();

            int d = calculateDate.DefinePeriodForSendingReports("ONE_MONTH");

            d.ShouldBeEquivalentTo(30);
        }
    }
}
