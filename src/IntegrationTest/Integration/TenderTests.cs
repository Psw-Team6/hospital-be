using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationLibrary.PDFReports.Service;
using IntegrationLibrary.Tender.Model;
using IntegrationLibrary.Tender.Service;
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
    public class TenderTests : BaseIntegrationTest
    {
        public TenderTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }
        private static TenderController SetupController(IServiceScope scope)
        {
            return new TenderController(scope.ServiceProvider.GetRequiredService<ITenderService>());
        }

        [Fact]
        public void CreateTender_Successfuly()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            Tender tender = new Tender();
            tender.Id = Guid.NewGuid();
            tender.HasDeadline=true;
            tender.DeadlineDate=DateTime.Now.AddDays(20);
            tender.Status = IntegrationLibrary.Enums.StatusTender.Open;
            tender.BloodUnitAmount = new List<BloodUnitAmount>();
            BloodUnitAmount bloodUnitAmount = new BloodUnitAmount();
            bloodUnitAmount.BloodType = IntegrationLibrary.BloodRequests.Model.BloodType.Apos;
            bloodUnitAmount.Amount = 20;
            tender.BloodUnitAmount.Append(bloodUnitAmount);
            var result = controller.Create(tender);
            result.ShouldBeOfType<ObjectResult>();
        }

        [Fact]
        public void CreateTender_Unsuccessfuly()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            Tender tender = null;
            var result = controller.Create(tender);
            result.ShouldBeOfType<BadRequestResult>();
        }

    }
}
