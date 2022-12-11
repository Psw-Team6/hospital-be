using System;
using System.Collections.Generic;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Service;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;

namespace HospitalTest.AppointmentPdfReportTest
{
    public class AppointmentPdfReportTest
    {
        
        [Fact]
        public void Appointment_doesnt_exist()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockPdfService = new Mock<IGeneratePdfReportService>();
            mockUnitOfWork.Setup(uw => uw.AppointmentRepository
                    .GetAppointmentsById(appointment.Id))
                .ReturnsAsync(() => null);
            
            AppointmentService service = new AppointmentService(mockUnitOfWork.Object,mockEmailService.Object,mockPdfService.Object);
            var res = service.GetAppointmentPdfReport(appointment.Id, _options).Result;
            
            Assert.Null(res);
        }

        [Fact]
        public void Examination_doesnt_exist()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockPdfService = new Mock<IGeneratePdfReportService>();
            mockUnitOfWork.Setup(uw => uw.AppointmentRepository
                    .GetAppointmentsById(appointment.Id))
                .ReturnsAsync(appointment);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository
                    .GetExaminationByAppointment(appointment))
                .ReturnsAsync(() => null);
            
            AppointmentService service = new AppointmentService(mockUnitOfWork.Object,mockEmailService.Object,mockPdfService.Object);
            var res = service.GetAppointmentPdfReport(appointment.Id, _options).Result;
            
            Assert.Null(res);
        }

        [Fact]
        public void Patient_is_anonymized()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockPdfService = new Mock<IGeneratePdfReportService>();
            mockUnitOfWork.Setup(uw => uw.ExaminationPrescriptionRepository
                    .GetPrescriptionById(prescription.Id))
                .ReturnsAsync(prescription);
            
            AppointmentService service = new AppointmentService(mockUnitOfWork.Object,mockEmailService.Object,mockPdfService.Object);
            var res = service.PrepareData(examination, _options_anonymized);
            Assert.Null(res.Appointment.Patient);
            
        }
        
        [Fact]
        public void Patient_is_not_anonymized()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockPdfService = new Mock<IGeneratePdfReportService>();
            mockUnitOfWork.Setup(uw => uw.ExaminationPrescriptionRepository
                    .GetPrescriptionById(prescription1.Id))
                .ReturnsAsync(prescription1);
            
            AppointmentService service = new AppointmentService(mockUnitOfWork.Object,mockEmailService.Object,mockPdfService.Object);
            var res = service.PrepareData(examination1, _options);
            Assert.NotNull(res.Appointment.Patient);
            
        }
        
        [Fact]
        public void Report_without_symptoms_successfuly()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockPdfService = new Mock<IGeneratePdfReportService>();
            mockUnitOfWork.Setup(uw => uw.ExaminationPrescriptionRepository
                    .GetPrescriptionById(prescription.Id))
                .ReturnsAsync(prescription);
            
            AppointmentService service = new AppointmentService(mockUnitOfWork.Object,mockEmailService.Object,mockPdfService.Object);
            var res = service.SetupDataBasedOnOptions( _options_without_symptoms,examination);
            Assert.Null(res.Symptoms);
            
        }
        
        [Fact]
        public void Report_without_prescriptions_successfuly()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockPdfService = new Mock<IGeneratePdfReportService>();
            mockUnitOfWork.Setup(uw => uw.ExaminationPrescriptionRepository
                    .GetPrescriptionById(prescription.Id))
                .ReturnsAsync(prescription);
            
            AppointmentService service = new AppointmentService(mockUnitOfWork.Object,mockEmailService.Object,mockPdfService.Object);
            var res = service.SetupDataBasedOnOptions( _options_without_prescriptions,examination);

            Assert.Null(res.Prescriptions);
            
        }
        
        [Fact]
        public void Appointment_pdf_report_create_successfuly()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockPdfService = new Mock<IGeneratePdfReportService>();
            mockUnitOfWork.Setup(uw => uw.AppointmentRepository
                    .GetAppointmentsById(appointment.Id))
                .ReturnsAsync(appointment);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository
                    .GetExaminationByAppointment(appointment))
                .ReturnsAsync(examination1);
            mockUnitOfWork.Setup(uw => uw.ExaminationPrescriptionRepository
                    .GetPrescriptionById(prescription.Id))
                .ReturnsAsync(prescription);
            
            AppointmentService service = new AppointmentService(mockUnitOfWork.Object,mockEmailService.Object,mockPdfService.Object);
            var res = service.GetAppointmentPdfReport(appointment.Id, _options).Result;
            
            Assert.NotNull(res);
        }
        
        //Test data
        private static AppointmentReportPdfRequest _request = new()
        {
            Anonymized = false,
            Presciptions = true,
            Symptoms = true
        };
        private static AppointmentReportPdfOptions _options = new()
        {
            Anonymized = false,
            Presciptions = true,
            Symptoms = true
        };
        
        private static AppointmentReportPdfOptions _options_anonymized = new()
        {
            Anonymized = true,
            Presciptions = true,
            Symptoms = true
        };
        
        private static AppointmentReportPdfOptions _options_without_symptoms = new()
        {
            Anonymized = false,
            Presciptions = true,
            Symptoms = false
        };
        
        private static AppointmentReportPdfOptions _options_without_prescriptions = new()
        {
            Anonymized = false,
            Presciptions = false,
            Symptoms = true
        };
        
        ///Test repository data
        static Appointment appointment = new()
        {
            Id = Guid.NewGuid(),
            Emergent = false,
            AppointmentType = AppointmentType.Examination,
            AppointmentState = AppointmentState.Pending,
            Duration = new DateRange
            {
                From = DateTime.Now,
                To = DateTime.Now.AddMinutes(30)
            },
            Patient = new Patient()
            {
                
            }
        };
        static Appointment appointment1 = new()
        {
            Id = Guid.NewGuid(),
            Emergent = false,
            AppointmentType = AppointmentType.Examination,
            AppointmentState = AppointmentState.Pending,
            Duration = new DateRange
            {
                From = DateTime.Now,
                To = DateTime.Now.AddMinutes(30)
            },
            Patient = new Patient()
            {
                
            }
        };
        static Symptom symptom = new()
        {
            Id = new Guid(),
            Description = "Polen"
        };
        static List<Symptom> symptoms = new List<Symptom> {symptom};
        static Medicine medicine = new Medicine
        {
            Id = default,
            Name = "Brufen",
            Amount = 0,
        };
        static List<Medicine> medicines = new List<Medicine> {medicine};
        static ExaminationPrescription prescription = new ExaminationPrescription(medicines,"one gram per day");
        static ExaminationPrescription prescription1 = new ExaminationPrescription(medicines,"one gram per day");
        static List<ExaminationPrescription>  prescriptions = new List<ExaminationPrescription> {prescription};
        static List<ExaminationPrescription>  prescriptions1 = new List<ExaminationPrescription> {prescription1};
        static Examination examination = new Examination(appointment, "aaa", symptoms,prescriptions);
        static Examination examination1 = new Examination(appointment1, "aaa", symptoms,prescriptions1);
        
    }
    
   
}