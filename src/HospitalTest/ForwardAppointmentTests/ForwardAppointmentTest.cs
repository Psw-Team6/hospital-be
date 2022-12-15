using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Doctors.Service;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Holidays.Repository;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;

namespace HospitalTest.ForwardAppointmentTests
{
    public class ForwardAppointmentTest
    {
        
        [Fact]
        public async Task Forward_appointment_doctor_with_specialisation_doesnt_exist()
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(uw => uw.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(uw => uw.DoctorRepository
                    .GetBySpecificSpecialisation(It.IsAny<String>()))
                .ReturnsAsync(() => null);
            var doctorService = new DoctorService(mockUnitOfWork.Object);
            Func<Task> act = () => doctorService.GetBySpecialisation("Surgeon");
            var ex = await Assert.ThrowsAsync<DoctorNotExist>(act);
            Assert.Equal("Doctors with this specialisation dont exist.", ex.Message);
        }

        [Fact]
        public async Task Forward_appointment_doctor_doesnt_exist()
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(uw => uw.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(uw => uw.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);
            var doctorService = new DoctorService(mockUnitOfWork.Object);
            DateRange Duration = new DateRange
            {
                From = new DateTime(2023, 10, 27, 15, 0, 0),
                To = new DateTime(2023, 10, 28, 15, 15, 0)
            };
            Guid Id = Guid.NewGuid();

        Func<Task> act = () => doctorService.generateFreeTimeSpans(Duration,Id);
            var ex = await Assert.ThrowsAsync<DoctorNotExist>(act);
            Assert.Equal("Doctor does not exist.", ex.Message);
        }
        
        [Fact]
        public async Task Forward_appointment_date_range_not_valid()
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(uw => uw.DoctorRepository).Returns(mockDoctorRepo.Object);
            DateRange Duration = new DateRange
            {
                From = new DateTime(2023, 11, 27, 15, 0, 0),
                To = new DateTime(2023, 10, 27, 13, 15, 0)
            };
            Guid Id = Guid.NewGuid();
            var doctorService = new DoctorService(mockUnitOfWork.Object);
            Func<Task> act = () => doctorService.generateFreeTimeSpans(Duration,Id);
            var ex = await Assert.ThrowsAsync<DateRangeException>(act);
            Assert.Equal("Date range is not valid", ex.Message);
        }
        
        [Fact]
        public async Task Forward_appointment_date_range_has_pased()
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(uw => uw.DoctorRepository).Returns(mockDoctorRepo.Object);
            DateRange Duration = new DateRange
            {
                From = new DateTime(2021, 10, 27, 15, 0, 0),
                To = new DateTime(2021, 11, 27, 13, 15, 0)
            };
            Guid Id = Guid.NewGuid();
            var doctorService = new DoctorService(mockUnitOfWork.Object);
            Func<Task> act = () => doctorService.generateFreeTimeSpans(Duration,Id);
            var ex = await Assert.ThrowsAsync<DateRangeNotValid>(act);
            Assert.Equal("Please select upcoming date", ex.Message);
        }
        
        [Fact]
        public async Task Forward_appointment_no_free_appointments()
        {
            DateRange Duration = new DateRange
            {
                From = new DateTime(2024, 3, 27, 15, 0, 0),
                To = new DateTime(2024, 3, 28, 13, 15, 0)
            };
            var doctor = CreateMockForwardNoFreeeAppointments(out var doctorService);
            Func<Task> act = () => doctorService.generateFreeTimeSpans(Duration,doctor.Id);
            var ex = await Assert.ThrowsAsync<DoctorIsNotAvailable>(act);
            Assert.Equal("No free appointments found for doctor in that period.", ex.Message);
        }
        
        [Fact]
        public async Task Forward_appointmet_sucsess()
        {
            DateRange Duration = new DateRange
            {
                From = new DateTime(2025, 11, 27, 15, 0, 0),
                To = new DateTime(2025, 11, 28, 13, 15, 0)
            };
            var doctor = CreateMockForwardAppointmentValid(out var doctorService);
            Func<Task> act = () => doctorService.generateFreeTimeSpans(Duration,doctor.Id);
            Assert.NotNull(act);
        }
        
        private static Doctor SeedDataDoctor()
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
                    From = new DateTime(2022, 10, 27, 8, 0, 0),
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
                Email = "Cajons@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222"
            };
            return doctor1;
        }
        
        private static Doctor SeedDataDoctorandHoliday(out Holiday holiday)
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
                Email = "Cajons@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222"
            };
            holiday = new()
            {
                Id = Guid.NewGuid(),
                IsUrgent = false,
                Description = "I want to go to luna park.",
                Doctor = doctor1,
                HolidayStatus = HolidayStatus.Pending,
                DateRange = new DateRange()
                {
                    From = new DateTime(2024, 3, 26, 15, 0, 0),
                    To = new DateTime(2024, 3, 29, 15, 30, 0)
                }
            };
            return doctor1;
        }
        
        private static Doctor CreateMockForwardAppointmentValid(out DoctorService doctorService)
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockAppointmentRepo = new Mock<IAppointmentRepository>();
            var mockHolidayRepo = new Mock<IHolidayRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor1 = SeedDataDoctor();
            DateRange range = new DateRange
            {
                From = new DateTime(2024, 10, 27, 15, 0, 0),
                To = new DateTime(2024, 10, 27, 16, 15, 0)
            };
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor1);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetDoctorWorkingSchedule(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor1.WorkingSchedule);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                .GetBySpecificSpecialisation(It.IsAny<String>()))
                .ReturnsAsync(() => new List<Doctor>(){doctor1});
            
            mockUnitOfWork.Setup(x => x.AppointmentRepository
                    .GetAllAppointmentsForDoctor(It.IsAny<Guid>()))
                .ReturnsAsync(() => new List<Appointment>());
            
            mockUnitOfWork.Setup(x => x.HolidayRepository).Returns(mockHolidayRepo.Object);
            mockUnitOfWork.Setup(x => x.HolidayRepository
                    .GetAllHolidaysForDoctor(It.IsAny<Guid>()))
                .ReturnsAsync(() => new List<Holiday>());
            doctorService = new DoctorService(mockUnitOfWork.Object);
            return doctor1;
        }
        private static Doctor CreateMockForwardNoFreeeAppointments(out DoctorService doctorService)
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockAppointmentRepo = new Mock<IAppointmentRepository>();
            var mockHolidayRepo = new Mock<IHolidayRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor1 = SeedDataDoctorandHoliday(out Holiday holiday);
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor1);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetDoctorWorkingSchedule(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor1.WorkingSchedule);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetBySpecificSpecialisation(It.IsAny<String>()))
                .ReturnsAsync(() => new List<Doctor>(){doctor1});
            
            mockUnitOfWork.Setup(x => x.AppointmentRepository
                    .GetAllAppointmentsForDoctor(It.IsAny<Guid>()))
                .ReturnsAsync(() => new List<Appointment>());
            
            mockUnitOfWork.Setup(x => x.HolidayRepository).Returns(mockHolidayRepo.Object);
            mockUnitOfWork.Setup(x => x.HolidayRepository
                    .GetAllHolidaysForDoctor(It.IsAny<Guid>()))
                .ReturnsAsync(() => new List<Holiday>(){holiday});
            doctorService = new DoctorService(mockUnitOfWork.Object);
            return doctor1;
        }
    }
}