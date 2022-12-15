using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Controllers;
using HospitalAPI.Controllers.Private;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.Examinations.Service;
using HospitalLibrary.Medicines.Service;
using HospitalTest.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace HospitalTest.ExaminationTests
{
    public class ExaminationIntegrationTest:BaseIntegrationTest
    {
        private static readonly Guid IdAppointmentCanBeCreated = new("852fa040-a1f5-46f1-963a-2addf5a86a06");
        private static readonly Guid IdAppointmentBadDate = new("852fa040-a1f5-46f1-963a-2addf5a86a37");
        private static readonly Guid IdAppointmentBadState = new("852fa040-a1f5-46f1-963a-2addf5a86a90");
        private const string AppUrl = "http://localhost:5000";
        public ExaminationIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }
        private static ExaminationController SetupExaminationController(IServiceScope scope)
        {
            return new ExaminationController(scope.ServiceProvider.GetRequiredService<ExaminationService>(),
                scope.ServiceProvider.GetRequiredService<IMapper>());
        }
        private static SymptomController SetupSymptomController(IServiceScope scope)
        {
            return new SymptomController(scope.ServiceProvider.GetRequiredService<SymptomService>(),
                scope.ServiceProvider.GetRequiredService<IMapper>());
        }
        private static MedicineController SetupMedicineController(IServiceScope scope)
        {
            return new MedicineController(scope.ServiceProvider.GetRequiredService<MedicineService>(),
                scope.ServiceProvider.GetRequiredService<IMapper>());
        }
        [Fact]
        public async Task CreateExamination_Success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controllerExamination = SetupExaminationController(scope);
            var controllerSymptoms = SetupSymptomController(scope);
            var controllerMedicine = SetupMedicineController(scope);
            var symptoms = await controllerSymptoms.GetAllSymptoms();
            var symptomsResult = symptoms.Value;
            var medicines = await controllerMedicine.GetAllForExamination();
            var medicinesResult = medicines.Value;
            var examinationRequest = new ExaminationRequest
            {
                IdApp = IdAppointmentCanBeCreated,
                Symptoms = symptomsResult,
                Prescriptions = new List<ExaminationPrescriptionRequest>
                {
                    new()
                    {
                        Usage = "Test",
                        Medicines = medicinesResult
                    }
                },
                Anamnesis = "Test"
            };
            //Act
            var result = await controllerExamination.CreateExamination(examinationRequest);
            // Assert
            result.Result.ShouldBeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task CreateExamination_Bad_Anamnesis()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controllerSymptoms = SetupSymptomController(scope);
            var controllerMedicine = SetupMedicineController(scope);
            var symptoms = await controllerSymptoms.GetAllSymptoms();
            var symptomsResult = symptoms.Value;
            var medicines = await controllerMedicine.GetAllForExamination();
            var medicinesResult = medicines.Value;
            var examinationRequest = new ExaminationRequest
            {
                IdApp = IdAppointmentCanBeCreated,
                Symptoms = symptomsResult,
                Prescriptions = new List<ExaminationPrescriptionRequest>
                {
                    new()
                    {
                        Usage = "Test",
                        Medicines = medicinesResult
                    }
                },
                Anamnesis = ""
            };
            var app = new HostApp();
            var client = app.CreateClient();
            client.BaseAddress = new Uri(AppUrl);
            //Act
            var response = await client.PostAsJsonAsync("/api/v1/Examination",examinationRequest);
            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task CreateExamination_Bad_Prescription()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controllerSymptoms = SetupSymptomController(scope);
            var controllerMedicine = SetupMedicineController(scope);
            var symptoms = await controllerSymptoms.GetAllSymptoms();
            var symptomsResult = symptoms.Value;
            var medicines = await controllerMedicine.GetAllForExamination();
            var medicinesResult = medicines.Value;
            var examinationRequest = new ExaminationRequest
            {
                IdApp = IdAppointmentCanBeCreated,
                Symptoms = symptomsResult,
                Prescriptions = new List<ExaminationPrescriptionRequest>
                {
                    new()
                    {
                        Usage = "",
                        Medicines = medicinesResult
                    }
                },
                Anamnesis = "Test"
            };
            var app = new HostApp();
            var client = app.CreateClient();
            client.BaseAddress = new Uri(AppUrl);
            //Act
            var response = await client.PostAsJsonAsync("/api/v1/Examination",examinationRequest);
            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task CreateExamination_Bad_Appointment_Date()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controllerSymptoms = SetupSymptomController(scope);
            var controllerMedicine = SetupMedicineController(scope);
            var symptoms = await controllerSymptoms.GetAllSymptoms();
            var symptomsResult = symptoms.Value;
            var medicines = await controllerMedicine.GetAllForExamination();
            var medicinesResult = medicines.Value;
            var examinationRequest = new ExaminationRequest
            {
                IdApp = IdAppointmentBadDate,
                Symptoms = symptomsResult,
                Prescriptions = new List<ExaminationPrescriptionRequest>
                {
                    new()
                    {
                        Usage = "Test",
                        Medicines = medicinesResult
                    }
                },
                Anamnesis = "Test"
            };
            var app = new HostApp();
            var client = app.CreateClient();
            client.BaseAddress = new Uri(AppUrl);
            //Act
            var response = await client.PostAsJsonAsync("/api/v1/Examination",examinationRequest);
            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task CreateExamination_Bad_Appointment_State()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controllerSymptoms = SetupSymptomController(scope);
            var controllerMedicine = SetupMedicineController(scope);
            var symptoms = await controllerSymptoms.GetAllSymptoms();
            var symptomsResult = symptoms.Value;
            var medicines = await controllerMedicine.GetAllForExamination();
            var medicinesResult = medicines.Value;
            var examinationRequest = new ExaminationRequest
            {
                IdApp = IdAppointmentBadState,
                Symptoms = symptomsResult,
                Prescriptions = new List<ExaminationPrescriptionRequest>
                {
                    new()
                    {
                        Usage = "Test",
                        Medicines = medicinesResult
                    }
                },
                Anamnesis = "Test"
            };
            var app = new HostApp();
            var client = app.CreateClient();
            client.BaseAddress = new Uri(AppUrl);
            //Act
            var response = await client.PostAsJsonAsync("/api/v1/Examination",examinationRequest);
            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}