using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Controllers;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.ApplicationUsers.Service;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Service;
using HospitalTest.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace HospitalTest.HospitalizationTest
{
    public class HospitalizationTest : BaseIntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private const string StaffUrl = "https://doctor-portal";
        private const string AppUrl = "http://localhost:5000";
        
        public HospitalizationTest(TestDatabaseFactory factory, ITestOutputHelper testOutputHelper) : base(factory)
        {
            _testOutputHelper = testOutputHelper;

        }
        
        private static PatientAdmissionController SetupController(IServiceScope scope)
        {
            return new PatientAdmissionController(scope.ServiceProvider.GetRequiredService<PatientAdmissionService>(),
                scope.ServiceProvider.GetRequiredService<IMapper>());
        }
        
        private static PatientController SetupPatientController(IServiceScope scope)
        {
            return new PatientController(scope.ServiceProvider.GetRequiredService<PatientService>(),
                scope.ServiceProvider.GetRequiredService<IMapper>());
        }
        
        [Fact]
        public async Task HospitalizePatient_Success()
        {
            // Arrange
            //using var scope = Factory.Services.CreateScope();
            //var controller = SetupController(scope);
            //var patientController = SetupPatientController(scope);
            //IEnumerable<Patient> patients = patientController.GetAllHospitalizedPatients() as IEnumerable<Patient>;
            // Act
            //var result = await controller.CreateAdmission(new PatientAdmissionRequest(
            //{
            
            //});
            // Assert
           // result.Result.ShouldBeOfType<OkObjectResult>();
        }
        
        
    }
}