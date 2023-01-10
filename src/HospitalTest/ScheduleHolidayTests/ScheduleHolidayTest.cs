using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Holidays.Repository;
using HospitalLibrary.Holidays.Service;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;

namespace HospitalTest.ScheduleHolidayTests
{
    public class ScheduleHolidayTest
    {

        [Fact]
        public async Task Schedule_holiday_doctor_doesnt_exist()
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var doctor1 = SeedDataDoctor(out Holiday holiday);
            mockUnitOfWork.Setup(uw => uw.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(uw => uw.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(() => null);
            var holidayService = new HolidayService(mockUnitOfWork.Object);
            Func<Task> act = () => holidayService.ScheduleHoliday(holiday);
            var ex = await Assert.ThrowsAsync<DoctorNotExist>(act);
            Assert.Equal("Doctor does not exist.", ex.Message);
        }

        [Fact]
        public async Task Schedule_holiday_date_range_not_valid()
        {
            var holiday = CreateMockNotValidDate(out var holidayService);
            Func<Task> act = () => holidayService.ScheduleHoliday(holiday);
            var ex = await Assert.ThrowsAsync<DateRangeException>(act);
            Assert.Equal("Date range is not valid",ex.Message);
        }
        [Fact]
        public async Task Schedule_holiday_select_upcoming_date()
        {
            var holiday = CreateMockNotValidDatePast(out var holidayService);
            Func<Task> act = () => holidayService.ScheduleHoliday(holiday);
            var ex = await Assert.ThrowsAsync<DateRangeNotValid>(act);
            Assert.Equal("Please select upcoming date",ex.Message);
        }
        
        [Fact]
        public void  Schedule_holiday_sucsess()
        {
            var holiday = CreateMockHolidazValid(out var holidayService);
            Func<Task> act = () => holidayService.ScheduleHoliday(holiday);
            Assert.NotNull(act);
        }

        [Fact]
        public async Task Schedule_holiday_over_appointment()
        {
            var holiday = CreateMockAllredyScheduledApp(out var holidayService);
            Func<Task> act = () => holidayService.ScheduleHoliday(holiday);
            var ex = await Assert.ThrowsAsync<DoctorIsNotAvailable>(act);
            Assert.Equal("You have already scheduled appointment in that period.",ex.Message);
        }

        [Fact]
        public async Task Schedule_holiday_no_replacement_found()
        {
            var holiday = CreateMockNoReplacementsFound(out var holidayService);
            Func<Task> act = () => holidayService.ScheduleHoliday(holiday);
            var ex = await Assert.ThrowsAsync<DoctorIsNotAvailable>(act);
            Assert.Equal("No replacement found for appointments scheduled in that period.",ex.Message);
        }
        private static Appointment SeedDataAppointment()
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
            Room room1 = new()
            {
                Id = Guid.NewGuid()
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
            Appointment appointment = new( Guid.NewGuid())
            {
                Emergent = false,
                Doctor = doctor1,
                Patient = patient1,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = new DateTime(2023, 10, 27, 15, 0, 0),
                    To = new DateTime(2023, 10, 27, 15, 15, 0)
                }
            };
            return appointment;
        }

        private static Doctor SeedDataDoctor(out Holiday holiday)
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
                Email = "Cajons@gmail.com"
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
                    From = new DateTime(2023, 10, 27, 15, 0, 0),
                    To = new DateTime(2023, 10, 29, 15, 30, 0)
                }
            };
            return doctor1;
        }
        
        
        

        private static Holiday CreateMockNotValidDate(out HolidayService holidayService)
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor1 = SeedDataDoctor(out Holiday holiday);
            holiday.DateRange = new DateRange
            {
                From = new DateTime(2023, 10, 27, 15, 0, 0),
                To = new DateTime(2023, 10, 27, 13, 15, 0)
            };
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor1);
            holidayService = new HolidayService(mockUnitOfWork.Object);
            return holiday;
        }
        
        private static Holiday CreateMockNotValidDatePast(out HolidayService holidayService)
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor1 = SeedDataDoctor(out Holiday holiday);
            holiday.DateRange = new DateRange
            {
                From = new DateTime(2021, 10, 27, 15, 0, 0),
                To = new DateTime(2021, 10, 27, 16, 15, 0)
            };
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor1);
            holidayService = new HolidayService(mockUnitOfWork.Object);
            return holiday;
        }
        
        private static Holiday CreateMockHolidazValid(out HolidayService holidayService)
        {
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor1 = SeedDataDoctor(out Holiday holiday);
            holiday.DateRange = new DateRange
            {
                From = new DateTime(2024, 10, 27, 15, 0, 0),
                To = new DateTime(2024, 10, 27, 16, 15, 0)
            };
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor1);
            holidayService = new HolidayService(mockUnitOfWork.Object);
            return holiday;
        }

        private static Holiday CreateMockAllredyScheduledApp(out HolidayService holidayService)
        {
            var mockAppointmentRepo = new Mock<IAppointmentRepository>();
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockHolidayRepo = new Mock<IHolidayRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor = SeedDataDoctor(out Holiday holiday);
            var appointment = SeedDataAppointment();
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor);
            mockUnitOfWork.Setup(x => x.AppointmentRepository).Returns(mockAppointmentRepo.Object);
            mockUnitOfWork.Setup(x => x.AppointmentRepository
                    .GetAllAppointmentsForDoctor(It.IsAny<Guid>()))
                .ReturnsAsync(() => new []{appointment});
            mockUnitOfWork.Setup(x => x.HolidayRepository).Returns(mockHolidayRepo.Object);
            mockUnitOfWork.Setup(x => x.HolidayRepository
                    .GetAllHolidaysForDoctor(It.IsAny<Guid>()))
                .ReturnsAsync(() => new List<Holiday>());
            holidayService = new HolidayService(mockUnitOfWork.Object);
            return holiday;
        }
        
        private static Holiday CreateMockNoReplacementsFound(out HolidayService holidayService)
        {
            var mockAppointmentRepo = new Mock<IAppointmentRepository>();
            var mockDoctorRepo = new Mock<IDoctorRepository>();
            var mockHolidayRepo = new Mock<IHolidayRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var doctor = SeedDataDoctor(out Holiday holiday);
            holiday.IsUrgent = true;
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
            
            var appointment = SeedDataAppointment();
            mockUnitOfWork.Setup(x => x.DoctorRepository).Returns(mockDoctorRepo.Object);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => doctor);
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetAllDoctors())
                .ReturnsAsync(() =>new List<Doctor>(){doctor});
            mockUnitOfWork.Setup(x => x.DoctorRepository
                    .GetDoctorWorkingSchedule(It.IsAny<Guid>()))
                .ReturnsAsync(() =>workingSchedule1);
            mockUnitOfWork.Setup(x => x.AppointmentRepository).Returns(mockAppointmentRepo.Object);
            mockUnitOfWork.Setup(x => x.AppointmentRepository
                    .GetAllAppointmentsForDoctor(It.IsAny<Guid>()))
                .ReturnsAsync(() => new []{appointment});
            mockUnitOfWork.Setup(x => x.HolidayRepository).Returns(mockHolidayRepo.Object);
            mockUnitOfWork.Setup(x => x.HolidayRepository
                    .GetAllHolidaysForDoctor(It.IsAny<Guid>()))
                .ReturnsAsync(() => new List<Holiday>());
            holidayService = new HolidayService(mockUnitOfWork.Object);
            return holiday;
        }
    }
}