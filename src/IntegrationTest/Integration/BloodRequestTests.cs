using AutoMapper;
using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.BloodRequests.Model;
using IntegrationLibrary.BloodRequests.Service;
using IntegrationLibrary.HTTP;
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
    public class BloodRequestTests : BaseIntegrationTest
    {
        public BloodRequestTests(TestDatabaseFactory<Startup> factory):base(factory) { }

        private static BloodRequestController SetupController(IServiceScope scope)
        {
            return new BloodRequestController(scope.ServiceProvider.GetRequiredService<IBloodRequestService>());
        }

        [Fact]
        public void Approve_request_successful()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            BloodRequest request = controller.GetFirst();
            request.Status = Status.APPPROVED;

            var result = controller.Update(request);

            result.ShouldBeOfType<OkObjectResult>();
            
        }

        [Fact]
        public void Approve_request_unsuccessful()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            BloodRequest request = null;

            var result = controller.Update(request);

            result.ShouldBeOfType<BadRequestResult>();

        }
    }
}
