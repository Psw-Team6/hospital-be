using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Appointments.Service.EventStoreService;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Repository;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;

namespace HospitalTest.ScheduleAppointmentTests
{
    public class ScheduleAppointmentTest
    {
        
        [Fact]
        public async Task Schedule_appointment_doctor_doesnt_exist()
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockPatientRepo = new Mock<IPatientRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockEventStoreSchedulingAppointmentService = new Mock<EventStoreSchedulingAppointmentService>();

            var patient1 = SeedDataPateint(out var appointment);
            
            mockUnitOfWork.Setup(x => x.PatientRepository).Returns(mockPatientRepo.Object);
            mockUnitOfWork.Setup(x => x.PatientRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(patient1);
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(()=>null);
            var scheduleService = new ScheduleService(mockUnitOfWork.Object,mockEmailService.Object, mockEventStoreSchedulingAppointmentService.Object);
            Func<Task> act = () => scheduleService.ScheduleAppointment(appointment);
            var ex = await Assert.ThrowsAsync<DoctorNotExist>(act);
            Assert.Equal("Doctor does not exist",ex.Message);
        }
        
        [Fact]
        public async Task Schedule_appointment_invalid_date()
        {
            var appointment = CreateMockNotValidDate(out var scheduleService);
            Func<Task> act = () => scheduleService.ScheduleAppointment(appointment);
            var ex = await Assert.ThrowsAsync<DateRangeNotValid>(act);
            Assert.Equal("Please select upcoming date",ex.Message);
        }
        [Fact]
        public async Task Schedule_appointment_patient_doesnt_exist()
        {
            var appointment = CreateMockPateintDoestExists(out var scheduleService);
            Func<Task> act = () => scheduleService.ScheduleAppointment(appointment);
            var ex = await Assert.ThrowsAsync<DoctorException>(act);
            Assert.Equal("Patient does not exist",ex.Message);
        }
        [Fact]
        public async Task Schedule_appointment_doctor_not_working()
        {
            var appointment = SetupNotWorkingAppointment(out var scheduleService);
            Func<Task> act = () => scheduleService.ScheduleAppointment(appointment);
            var ex = await Assert.ThrowsAsync<DoctorIsNotAvailable>(act);
            Assert.Equal("You are not available.Check your schedule.",ex.Message);
        }

        private static Appointment SetupNotWorkingAppointment(out ScheduleService scheduleService)
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockPatientRepo = new Mock<IPatientRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockEventStoreSchedulingAppointmentService = new Mock<EventStoreSchedulingAppointmentService>();


            Address address = new()
            {
                Id = Guid.NewGuid(),
                City = "Novi Sad",
                StreetNumber = "23A",
                Country = "Serbia",
                Street = "Kosovska",
                Postcode = 21000
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
            WorkingSchedule workingSchedule1 = new()
            {
                Id = Guid.NewGuid(),
                ExpirationDate = new NullableDateRange
                {
                    From = new DateTime(2022, 10, 27),
                    To = new DateTime(2023, 1, 27)
                },
                DayOfWork = new DateRange()
                {
                    From = new DateTime(2022, 12, 27, 8, 0, 0),
                    To = new DateTime(2022, 12, 27, 14, 0, 0)
                }
            };
            Doctor doctor1 = new()
            {
                Id = Guid.NewGuid(),
                Address = address,
                WorkingSchedule = workingSchedule1,
                WorkingScheduleId = workingSchedule1.Id,
                Room = room1,
                Username = "Ilija",
                Password = "miki123",
                Name = "Ilija",
                Surname = "Maric",
                Email = "Cajons@gmail.com"
            };
            Patient patient1 = new()
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
                Patient = patient1,
                DoctorId = doctor1.Id,
                PatientId = patient1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = new DateTime(2023, 10, 27, 15, 0, 0),
                    To = new DateTime(2023, 10, 27, 15, 15, 0)
                },
            };
            mockUnitOfWork.Setup(x => x.PatientRepository).Returns(mockPatientRepo.Object);
            mockUnitOfWork.Setup(x => x.PatientRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => patient1);
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor1);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetDoctorWorkingSchedule(appointment.DoctorId))
                .ReturnsAsync(() => workingSchedule1);

            scheduleService = new ScheduleService(mockUnitOfWork.Object, mockEmailService.Object, mockEventStoreSchedulingAppointmentService.Object);
            return appointment;
        }

        private static Appointment CreateMockPateintDoestExists(out ScheduleService scheduleService)
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockPatientRepo = new Mock<IPatientRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockEventStoreSchedulingAppointmentService = new Mock<EventStoreSchedulingAppointmentService>();


            Address address = new()
            {
                Id = Guid.NewGuid(),
                City = "Novi Sad",
                StreetNumber = "23A",
                Country = "Serbia",
                Street = "Kosovska",
                Postcode = 21000
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
                    To = new DateTime(2023, 1, 27)
                },
                DayOfWork = new DateRange()
                {
                    From = new DateTime(2022, 10, 27, 8, 0, 0),
                    To = new DateTime(2022, 10, 27, 14, 0, 0)
                }
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
            Patient patient1 = new()
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
                Patient = patient1,
                DoctorId = doctor1.Id,
                PatientId = patient1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = new DateTime(2023, 11, 23, 15, 0, 0),
                    To = new DateTime(2023, 11, 23, 15, 15, 0)
                },
            };
            mockUnitOfWork.Setup(x => x.PatientRepository).Returns(mockPatientRepo.Object);
            mockUnitOfWork.Setup(x => x.PatientRepository
                    .GetByIdAsync(appointment.PatientId))
                .ReturnsAsync(() => null);
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(appointment.DoctorId))
                .ReturnsAsync(() => doctor1);
            scheduleService = new ScheduleService(mockUnitOfWork.Object, mockEmailService.Object, mockEventStoreSchedulingAppointmentService.Object);
            return appointment;
        }

        private static Patient SeedDataPateint(out Appointment appointment)
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
                    To = new DateTime(2023, 1, 27)
                },
                DayOfWork = new DateRange()
                {
                    From = new DateTime(2022, 10, 27, 8, 0, 0),
                    To = new DateTime(2022, 10, 27, 14, 0, 0)
                }
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
            Patient patient1 = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Sale",
                Password = "sale1312",
                Name = "Sale",
                Surname = "Lave",
                Email = "psw.isa.mail@gmail.com"
            };
            appointment = new Appointment(Guid.NewGuid())
            {
                Emergent = false,
                Doctor = doctor1,
                Patient = patient1,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = new DateTime(2022, 10, 27, 15, 0, 0),
                    To = new DateTime(2022, 10, 27, 15, 15, 0)
                }
            };
            return patient1;
        }
        private static Appointment CreateMockNotValidDate(out ScheduleService scheduleService)
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockPatientRepo = new Mock<IPatientRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockEmailService = new Mock<IEmailService>();
            var mockEventStoreSchedulingAppointmentService = new Mock<EventStoreSchedulingAppointmentService>();


            Address address = new()
            {
                Id = Guid.NewGuid(),
                City = "Novi Sad",
                StreetNumber = "23A",
                Country = "Serbia",
                Street = "Kosovska",
                Postcode = 21000
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
                    To = new DateTime(2023, 1, 27)
                },
                DayOfWork = new DateRange()
                {
                    From = new DateTime(2022, 10, 27, 8, 0, 0),
                    To = new DateTime(2022, 10, 27, 14, 0, 0)
                }
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
            Patient patient1 = new()
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
                Patient = patient1,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = new DateTime(2022, 10, 27, 15, 0, 0),
                    To = new DateTime(2022, 10, 27, 15, 15, 0)
                },
            };
            mockUnitOfWork.Setup(x => x.PatientRepository).Returns(mockPatientRepo.Object);
            mockUnitOfWork.Setup(x => x.PatientRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => patient1);
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor1);
            scheduleService = new ScheduleService(mockUnitOfWork.Object, mockEmailService.Object, mockEventStoreSchedulingAppointmentService.Object);
            return appointment;
        }
    }
}