using AutoMapper;
using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPI.ScheduleTask.Service;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;
using IntegrationLibrary.ConfigureGenerateAndSend.Service;
using IntegrationTest.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
        IMapper maper;
        private static ConfigureGenerateAndSendController SetupController(IServiceScope scope, IMapper maper)
        {
            return new ConfigureGenerateAndSendController(scope.ServiceProvider.GetRequiredService<IConfigureGenerateAndSendService>(),  maper);
        }

        [Fact]
        public void Edit_configuration_sucessfully()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, maper);
            ConfigureGenerateAndSend request = controller.GetFirst();
            var result = controller.Edit(request);

            result.ShouldBeOfType<BadRequestResult>();
        }

        public void Edit_configuration_unsucessfully_eonfiguration_not_changed_()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, maper);
            ConfigureGenerateAndSend request = controller.GetFirst();
            var result = controller.Edit(request);

            result.ShouldBeOfType<BadRequestResult>();
        }

        [Fact]
        public void Edit_configuration_unsucessfully_some_field_is_null()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, maper);
            ConfigureGenerateAndSend request = controller.GetFirst();
            request.BloodBankName = null;

            var result = controller.Edit(request);

            result.ShouldBeOfType<BadRequestResult>();
        }


        [Fact]
        public void Next_send_period_calculate_corect()
        {
            CalculateDate calculateDate = new CalculateDate();

            int d = calculateDate.DefinePeriodForSendingReports("ONE_MONTH");

            d.ShouldBeEquivalentTo(30);
        }
    }
}
