using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HospitalAPI.Controllers;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.ApplicationUsers.Service;
using HospitalTest.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace HospitalTest.LoginTests
{
    public class LoginTests : BaseIntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private const string StaffUrl = "https://doctor-portal";
        private const string PatientUrl = "https://patient-portal";
        private const string AppUrl = "http://localhost:5000";

        public LoginTests(TestDatabaseFactory factory, ITestOutputHelper testOutputHelper) : base(factory)
        {
            _testOutputHelper = testOutputHelper;
        }
        private static ApplicationUserController SetupController(IServiceScope scope)
        {
            return new ApplicationUserController(scope.ServiceProvider.GetRequiredService<ApplicationUserService>(),
                scope.ServiceProvider.GetRequiredService<IHttpClientFactory>());
        }

        [Fact]
        public async Task Authenticate_Patient_Success_Login_Result()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            // Act
            var result = await controller.Authenticate(new LoginRequest
            {
                Username = "Patient",
                Password = "123",
                PortalUrl = PatientUrl
            });
            // Assert
            result.Result.ShouldBeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task Authenticate_Doctor_Success__Login_Result()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            // Act
            var result = await controller.Authenticate(new LoginRequest
            {
                Username = "Doctor",
                Password = "123",
                PortalUrl = StaffUrl
            });
            // Assert
            result.Result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Authenticate_Manager_Success_Login_Result()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            // Act
            var result = await controller.Authenticate(new LoginRequest
            {
                Username = "Manager",
                Password = "123",
                PortalUrl = StaffUrl
            });
            // Assert
            result.Result.ShouldBeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task Authenticate_BloodBank_Success_Login_Result()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            // Act
            var result = await controller.Authenticate(new LoginRequest
            {
                Username = "BloodBank",
                Password = "123",
                PortalUrl = StaffUrl
            });
            // Assert
            result.Result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Authenticate_Patient_Cannot_Login_Into_Doctor_Portal()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            
            // Act
            var result = await controller.Authenticate(new LoginRequest
            {
                Username = "Patient",
                Password = "123",
                PortalUrl = StaffUrl
            });
            // Assert
            result.Result.ShouldBeOfType<BadRequestObjectResult>();
        }
        [Theory]
        [InlineData("Doctor11","12345")]
        [InlineData("Patient12","1234")]
        [InlineData("Manager13","123456")]
        [InlineData("BloodBank13","123456")]
        public async Task Authenticate_User_Bad_Credentials(string username,string password)
        {
            // Arrange
            var app = new HostApp();
            var client = app.CreateClient();
            client.BaseAddress = new Uri(AppUrl);
            //Act
            var req = new LoginRequest
            {
                Username = username,
                Password = password,
                PortalUrl = StaffUrl
            };
            var response = await client.PostAsJsonAsync("/api/v1/ApplicationUser/Authenticate",req);
            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            _testOutputHelper.WriteLine(response.Content.ToString());
        }
        [Theory]
        [InlineData("Doctor11","12345","")]
        [InlineData("","1234",StaffUrl)]
        [InlineData("","",StaffUrl)]
        [InlineData("Miki","",StaffUrl)]
        [InlineData(null,"12",PatientUrl)]
        public async Task Authenticate_User_Bad_Request(string username,string password,string url)
        {
            // Arrange
            var app = new HostApp();
            var client = app.CreateClient();
            client.BaseAddress = new Uri(AppUrl);
            //Act
            var req = new LoginRequest
            {
                Username = username,
                Password = password,
                PortalUrl = url
            };
            var response = await client.PostAsJsonAsync("/api/v1/ApplicationUser/Authenticate",req);
            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            _testOutputHelper.WriteLine(response.Content.ToString());
        }
        [Fact]
        public async Task Authenticate_Doctor_Cannot_Login_Into_Patient_Portal()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            // Act
            var result = await controller.Authenticate(new LoginRequest
            {
                Username = "Doctor",
                Password = "123",
                PortalUrl = PatientUrl
            });
            // Assert
            result.Result.ShouldBeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task Authenticate_Manager_Cannot_Login_Into_Patient_Portal()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            // Act
            var result = await controller.Authenticate(new LoginRequest
            {
                Username = "Manager",
                Password = "123",
                PortalUrl = PatientUrl
            });
            // Assert
            result.Result.ShouldBeOfType<BadRequestObjectResult>();
        }

        
    }
}