using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Repository;
using HospitalLibrary.Patients.Service;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Service;
using HospitalLibrary.TreatmentReports.Model;
using HospitalLibrary.TreatmentReports.Repository;
using Moq;
using Xunit;

namespace HospitalTest.DischargeHospitalizedPatientsTests
{
    public class DischargePatientsTest
    {
        [Fact]
        public async Task Discharge_patient_admission_doesnt_exist()
        {
            var mockUnitOfWork = ArrangeData(out var mockGeneratePdfService, out var mockRoomBedService, out var admission);
            mockUnitOfWork.Setup(uw => uw.PatientAdmissionRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);
            
            var admissionService =
                new PatientAdmissionService(mockUnitOfWork.Object, mockGeneratePdfService.Object, mockRoomBedService.Object);
            Task Act() => admissionService.DischargePatient(admission);
            var ex = await Assert.ThrowsAsync<PatientAdmissionException>(Act);
            
            Assert.Equal("Patient admission not found!",ex.Message);
        }

        [Fact]
        public async Task Discharge_patient_admission_patient_already_discharged()
        {
            var mockUnitOfWork = ArrangeData(out var mockGeneratePdfService, out var mockRoomBedService, out var admission);
            
            mockUnitOfWork.Setup(uw => uw.PatientAdmissionRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => admission);
            
            var admissionService =
                new PatientAdmissionService(mockUnitOfWork.Object, mockGeneratePdfService.Object, mockRoomBedService.Object);
            Task Act() => admissionService.DischargePatient(admission);
            var ex = await Assert.ThrowsAsync<PatientDischargeException>(Act);
            
            Assert.Equal("Patient is already discharged!",ex.Message);
        }

        [Fact]
        public async Task Discharge_patient_treatment_report_doesnt_exists()
        {
            var mockTreatmentReportRepo = new Mock<ITreatmentReportRepository>();

            var mockUnitOfWork = ArrangeDataTreatmentReport(out var mockGeneratePdfService, out var mockRoomBedService,
                out var admission);
            mockUnitOfWork.Setup(uw => uw.PatientAdmissionRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => admission);
            mockUnitOfWork.Setup(uw => uw.PatientAdmissionRepository
                    .GetPatientAdmissionByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => admission);

            mockUnitOfWork.Setup(uw => uw.TreatmentReportRepository).Returns(mockTreatmentReportRepo.Object);
            mockUnitOfWork.Setup(uw => uw.TreatmentReportRepository
                    .FindByPatientAdmission(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var admissionService =
                new PatientAdmissionService(mockUnitOfWork.Object, mockGeneratePdfService.Object,
                    mockRoomBedService.Object);
            Task Act() => admissionService.DischargePatient(admission);
            var ex = await Assert.ThrowsAsync<TreatmentReportException>(Act);
            
            Assert.Equal("Treatment report not found!", ex.Message);
        
    }

        private  Mock<IUnitOfWork> ArrangeDataTreatmentReport(out Mock<IGeneratePdfReportService> mockGeneratePdfService, out Mock<IRoomBedService> mockRoomBedService, out PatientAdmission admission)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockGeneratePdfService = new Mock<IGeneratePdfReportService>();
            mockRoomBedService = new Mock<IRoomBedService>();
            var mockPatientAdmission = new Mock<IPatientAdmissionRepository>();

            admission = SeedValidDataAdmissionTreatmentReport();
            mockUnitOfWork.Setup(uw => uw.PatientAdmissionRepository).Returns(mockPatientAdmission.Object);
            return mockUnitOfWork;
        }

        private PatientAdmission SeedValidDataAdmissionTreatmentReport()
        {
            var room = new Room
            {
                Id = Guid.NewGuid(),
                Name = null,
                Doctor = null,
                Beds = null,
                Equipments = null,
                Patients = null,
                FloorId = default,
                Floor = null,
                BuildingId = default,
                GRoomId = default
            };
        
            var bed = new RoomBed
            {
                Id = Guid.NewGuid(),
                IsFree = false,
                Number = null,
                Room = room,
                RoomId = room.Id,
                Patients = null
            };
            var admission = new PatientAdmission
            {
                Id = Guid.NewGuid(),
                DateOfAdmission = default,
                PatientId = default,
                Patient = null,
                SelectedBedId =bed.Id ,
                SelectedBed = bed,
                SelectedRoomId = room.Id,
                SelectedRoom = room,
                Reason = "bbb",
                ReasonOfDischarge = "a",
                DateOfDischarge =null
            };
            return admission;
        }

        [Fact]
        public async Task Discharge_patient_calls_generate_pdf()
        {
            var mockTreatmentReportRepo = new Mock<ITreatmentReportRepository>();
            
            var mockUnitOfWork = ArrangeValidData(out var mockGeneratePdfService, out var mockRoomBedService, out var admission, out var report);
            mockUnitOfWork.Setup(uw => uw.PatientAdmissionRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => admission);
            mockUnitOfWork.Setup(uw => uw.PatientAdmissionRepository
                    .GetPatientAdmissionByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => admission);
            
            mockUnitOfWork.Setup(uw => uw.TreatmentReportRepository).Returns(mockTreatmentReportRepo.Object);
            mockUnitOfWork.Setup(uw => uw.TreatmentReportRepository
                    .FindByPatientAdmission(It.IsAny<Guid>()))
                .ReturnsAsync(() => report);
            
            var admissionService =
                new PatientAdmissionService(mockUnitOfWork.Object, mockGeneratePdfService.Object, mockRoomBedService.Object);
            await admissionService.DischargePatient(admission);
            
            mockGeneratePdfService.Verify(x => x.GeneratePdfReport(admission,report),Times.Once());
        }

        private Mock<IUnitOfWork> ArrangeValidData(out Mock<IGeneratePdfReportService> mockGeneratePdfService, out Mock<IRoomBedService> mockRoomBedService, out PatientAdmission admission, out TreatmentReport report)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockGeneratePdfService = new Mock<IGeneratePdfReportService>();
            mockRoomBedService = new Mock<IRoomBedService>();
            var mockPatientAdmission = new Mock<IPatientAdmissionRepository>();

            admission = SeedValidDataAdmission(out  report);
            mockUnitOfWork.Setup(uw => uw.PatientAdmissionRepository).Returns(mockPatientAdmission.Object);
            return mockUnitOfWork;
        }

        private static PatientAdmission SeedValidDataAdmission(out TreatmentReport report)
        {
            var room = new Room
            {
                Id = Guid.NewGuid(),
                Name = null,
                Doctor = null,
                Beds = null,
                Equipments = null,
                Patients = null,
                FloorId = default,
                Floor = null,
                BuildingId = default,
                GRoomId = default
            };
        
            var bed = new RoomBed
            {
                Id = Guid.NewGuid(),
                IsFree = false,
                Number = null,
                Room = room,
                RoomId = room.Id,
                Patients = null
            };
         var admission = new PatientAdmission
            {
                Id = Guid.NewGuid(),
                DateOfAdmission = default,
                PatientId = default,
                Patient = null,
                SelectedBedId =bed.Id ,
                SelectedBed = bed,
                SelectedRoomId = room.Id,
                SelectedRoom = room,
                Reason = "bbb",
                ReasonOfDischarge = "a",
                DateOfDischarge =null
            };
         report = new TreatmentReport
         {
             Id = Guid.NewGuid(),
             PatientAdmissionId = admission.Id,
             PatientAdmission = admission,
             MedicinePrescriptions = null,
             BloodPrescriptions = null
         };
         return admission;
            
        }

        private Mock<IUnitOfWork> ArrangeData(out Mock<IGeneratePdfReportService> mockGeneratePdfService, out Mock<IRoomBedService> mockRoomBedService, out PatientAdmission admission)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockGeneratePdfService = new Mock<IGeneratePdfReportService>();
            mockRoomBedService = new Mock<IRoomBedService>();
            var mockPatientAdmission = new Mock<IPatientAdmissionRepository>();

            admission = SeedDataAdmission();
            mockUnitOfWork.Setup(uw => uw.PatientAdmissionRepository).Returns(mockPatientAdmission.Object);
            return mockUnitOfWork;
        }

        private PatientAdmission SeedDataAdmission()
        {
            PatientAdmission admission = new PatientAdmission
            {
                Id = Guid.NewGuid(),
                DateOfAdmission = default,
                PatientId = default,
                Patient = null,
                SelectedBedId = default,
                SelectedBed = null,
                SelectedRoomId = default,
                SelectedRoom = null,
                Reason = null,
                ReasonOfDischarge = "a",
                DateOfDischarge = new DateTime()
            };
            return admission;
        }
        
    }
}