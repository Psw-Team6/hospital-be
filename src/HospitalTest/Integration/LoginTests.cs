using System.Net.Http;
using AutoMapper;
using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalLibrary.ApplicationUsers.Service;
using HospitalTest.Setup;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HospitalTest.Integration
{
    public class LoginTests : BaseIntegrationTest
    {
        public LoginTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static ApplicationUserController SetupController(IServiceScope scope)
        {
            return new ApplicationUserController(scope.ServiceProvider.GetRequiredService<ApplicationUserService>(),
                scope.ServiceProvider.GetRequiredService<IHttpClientFactory>());
        }

        [Fact]
        public void Can_doctor_log_in()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
        }
    }
}