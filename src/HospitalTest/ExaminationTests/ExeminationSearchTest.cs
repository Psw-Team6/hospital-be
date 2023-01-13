using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Examinations.Repository;
using HospitalLibrary.Examinations.Repository.EventStoreRepository;
using HospitalLibrary.Examinations.Service;
using HospitalLibrary.Examinations.Service.EventStoreService;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Repository;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;
using Moq;
using SendGrid.Helpers.Errors.Model;
using Xunit;

namespace HospitalTest.ExaminationTests
{
    public class ExeminationSearchTest
    {

        [Fact]
        public async Task Search_exeminations_no_ex_found()
        {
            var mockExaminationRepo = new Mock<IExaminationRepository>();
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockPatientRepo = new Mock<IPatientRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor = SeedDataDoctorandHoliday(out Patient patient, out Examination examination);
            
            mockUnitOfWork.Setup(uw => uw.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(uw => uw.PatientRepository).Returns(mockPatientRepo.Object);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository).Returns(mockExaminationRepo.Object);

            
            mockUnitOfWork.Setup(uw => uw.DoctorRepository
                .GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => doctor);
            mockUnitOfWork.Setup(uw => uw.PatientRepository
                .GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => patient);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository
                .GetAllExaminations()).ReturnsAsync(() => new List<Examination>(){examination});
            var exeminationService = new ExaminationService(mockUnitOfWork.Object,new Mock<IEventStoreService>().Object);
            Func<Task> act = () => exeminationService.GetSearchedExaminations("nevalja");
            var ex = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal("No Exeminatiosn found", ex.Message);
        }
        
        [Fact]
        public async Task Search_exeminations_word_sucsses()
        {
            var mockExaminationRepo = new Mock<IExaminationRepository>();
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockPatientRepo = new Mock<IPatientRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor = SeedDataDoctorandHoliday(out Patient patient, out Examination examination);
            
            mockUnitOfWork.Setup(uw => uw.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(uw => uw.PatientRepository).Returns(mockPatientRepo.Object);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository).Returns(mockExaminationRepo.Object);

            
            mockUnitOfWork.Setup(uw => uw.DoctorRepository
                .GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => doctor);
            mockUnitOfWork.Setup(uw => uw.PatientRepository
                .GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => patient);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository
                .GetAllExaminations()).ReturnsAsync(() => new List<Examination>(){examination});
            var exeminationService = new ExaminationService(mockUnitOfWork.Object,new Mock<IEventStoreService>().Object);
            Func<Task> act = () => exeminationService.GetSearchedExaminations("Sale");
            Assert.NotNull(act);
        }
        
        [Fact]
        public async Task Search_exeminations_query_sucsses()
        {
            var mockExaminationRepo = new Mock<IExaminationRepository>();
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockPatientRepo = new Mock<IPatientRepository>();
            var mockEventStoreExaminationRepository = new Mock<EventStoreExaminationService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor = SeedDataDoctorandHoliday(out Patient patient, out Examination examination);
            
            mockUnitOfWork.Setup(uw => uw.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(uw => uw.PatientRepository).Returns(mockPatientRepo.Object);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository).Returns(mockExaminationRepo.Object);
            // mockUnitOfWork.Setup(uw => uw.EventStoreExaminationRepository).Returns(mockEventStoreExaminationRepository.Object);
            
            mockUnitOfWork.Setup(uw => uw.DoctorRepository
                .GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => doctor);
            mockUnitOfWork.Setup(uw => uw.PatientRepository
                .GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => patient);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository
                .GetAllExaminations()).ReturnsAsync(() => new List<Examination>(){examination});
            var exeminationService = new ExaminationService(mockUnitOfWork.Object,new Mock<IEventStoreService>().Object);
            Func<Task> act = () => exeminationService.GetSearchedExaminations("Sale Lave");
            Assert.NotNull(act);
        }
        
        
        private static Doctor SeedDataDoctorandHoliday(out Patient patient, out Examination examination)
        {
            Address address = new()
            {
                Id = Guid.NewGuid(),
                City = "Novi Sad",
                StreetNumber = "23A",
                Country = "Serbia",
                Street = "Kosovska",
                Postcode = 21000
            };
            
            Specialization specializationDermatology = new()
            {
                Id = Guid.NewGuid(),
                Name = "Dermatology"
            };

            WorkingSchedule workingSchedule1 = new()
            {
                Id = Guid.NewGuid(),
                ExpirationDate = new NullableDateRange
                {
                    From = new DateTime(2022, 10, 27),
                    To = new DateTime(2024, 1, 27)
                },
                DayOfWork = new DateRange()
                {
                    From = new DateTime(2024, 10, 27, 8, 0, 0),
                    To = new DateTime(2024, 12, 27, 14, 0, 0)
                }
            };
            Building building1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Stara bolnica"
            };
            Floor floor11 = new()
            {
                Id= Guid.NewGuid(),
                Name = "F0",
                FloorNumber = 0,
                BuildingId = building1.Id
            };
            
            Room room1 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "A11",
                BuildingId = floor11.BuildingId
            };
            Doctor doctor1 = new()
            {
                Id = Guid.NewGuid(),
                Specialization = specializationDermatology,
                Address = address,
                WorkingSchedule = workingSchedule1,
                Room = room1,
                Username = "Ilija",
                Password = "miki123",
                Name = "Ilija",
                Surname = "Maric",
                Email = "Cajons@gmail.com"
            };
            patient = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Sale",
                Password = "sale1312",
                Name = "Sale",
                Surname = "Lave",
                Email = "psw.isa.mail@gmail.com"
            };
            Appointment appointment = new(Guid.NewGuid())
            {
                Emergent = false,
                Doctor = doctor1,
                Patient = patient,
                DoctorId = doctor1.Id,
                PatientId = patient.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = new DateTime(2023, 10, 27, 15, 0, 0),
                    To = new DateTime(2023, 10, 27, 15, 15, 0)
                },
            };
            examination = new()
            {
                Appointment = appointment,
                Prescriptions = new List<ExaminationPrescription>(),
                Symptoms = new List<Symptom>()
                
            };
            return doctor1;

        }
    }
    
    
}