using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Holidays.Service;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.sharedModel;
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
            Func<Task> hol = () => holidayService.ScheduleHoliday(holiday);
            var ex = await Assert.ThrowsAsync<DoctorNotExist>(hol);
            Assert.Equal("Doctor does not exist.", ex.Message);
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
                ExpirationDate = new DateRange
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
                    From = new DateTime(2022, 10, 27, 15, 0, 0),
                    To = new DateTime(2022, 10, 27, 15, 15, 0)
                }
            };
            return doctor1;
        }
    }
}