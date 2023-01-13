using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Consiliums.Repository;
using HospitalLibrary.Consiliums.Service;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Service;
using HospitalLibrary.Enums;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;

namespace HospitalTest.ScheduleConsiliumTests
{
    public class ScheduleConsiliumLogicTests
    {
        [Fact]
        public void Schedule_Consilium_Time_Range_Not_Found()
        {
            // Arrange
            var mockService = new Mock<IConsiliumService>();
            mockService.Setup(s =>
                    s.FindTimeRangeForAllDoctors(new Consilium()))
                .Returns(() => null);
            
            // Act
            var service = mockService.Object;
            try
            {
                var result = service.FindTimeRangeForAllDoctors(new Consilium());
            }
            catch (ConsiliumException e)
            {
                Assert.Equal("The system couldn't find a date and time in that time range!" ,e.Message);
            }
        }
        [Fact]
        public void Schedule_Consilium_Time_Range_Found()
        {
            // Arrange
            var timeRange = new TimeRange();
            var consilium = SeedDataConsilium();
            var mockService = new Mock<IConsiliumService>();
            mockService.Setup(s =>
                    s.FindTimeRangeForAllDoctors(consilium))
                .Returns(() => timeRange);
            
            // Act
            var service = mockService.Object;
            var result = service.FindTimeRangeForAllDoctors(consilium);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Schedule_Consilium_Meeting_Room_Found()
        {
                // Arrange
                var start = new DateTime(2022, 12, 17);
                var end = new DateTime(2022, 12, 19);
                var timeRange = new TimeRange
                {
                    From = start,
                    To = end,
                    Duration = 60
                };
                var room = new Room();
                var consilium = SeedDataConsilium();
                var mockService = new Mock<IConsiliumService>();
                mockService.Setup(s =>
                        s.FindAvailableMeetingRoom(timeRange))
                    .ReturnsAsync(() => room);
            
                // Act
                var service = mockService.Object;
                var result = await service.FindAvailableMeetingRoom(timeRange);
                Assert.NotNull(result);
        }
        [Fact]
        public async Task Schedule_Consilium_Meeting_Room_Not_Found()
        {
            // Arrange
            var start = new DateTime(2022, 12, 17);
            var end = new DateTime(2022, 12, 19);
            var timeRange = new TimeRange
            {
                From = start,
                To = end,
                Duration = 60
            };
            var room = new Room();
            var consilium = SeedDataConsilium();
            var mockService = new Mock<IConsiliumService>();
            mockService.Setup(s =>
                    s.FindAvailableMeetingRoom(timeRange))
                .ReturnsAsync(() => null);
            
            // Act
            var service = mockService.Object;
            var result = await service.FindAvailableMeetingRoom(timeRange);
            Assert.Null(result);
        }
        [Fact]
        public async Task Schedule_Consilium_Success()
        {
            var mockService = new Mock<IConsiliumService>();
            var consilium = SeedDataConsilium2(out var room, out var doctorList);
            mockService.Setup(s =>
                    s.ScheduleConsilium(consilium))
                .ReturnsAsync(() => consilium);
            
            // Act
            var service = mockService.Object;
            var result = await service.ScheduleConsilium(consilium);
            
            Assert.NotNull(result);
        }
        private Consilium SeedDataConsilium()
        {
            var duration = new DateRange
            {
                From = new DateTime(),
                To = new DateTime()
            };
            Specialization specializationDermatology = new()
            {
                Id = Guid.NewGuid(),
                Name = "Dermatology"
            };
            var theme = "Theme";
            var appointment1 = new Appointment( Guid.NewGuid())
            {
                Duration = duration
            };
            var holiday = new Holiday
            {
                DateRange = duration
            };
            var holidayList = new List<Holiday>();
            holidayList.Add(holiday);
            var appointmentList = new List<Appointment>();
            appointmentList.Add(appointment1);
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
            Room room2 = new()
            {
                Id = Guid.NewGuid()
            };
            var start = new DateTime(2022, 12, 17);
            var end = new DateTime(2022, 12, 19);
            var timeRange = new TimeRange
            {
                From = start,
                To = end,
                Duration = 60
            };
            var doctor1 = new Doctor
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
                Appointments = appointmentList,
                Holidays = holidayList
            };
            var doctor2 = new Doctor
            {
                Id = Guid.NewGuid(),
                Specialization = specializationDermatology,
                Address = address,
                WorkingSchedule = workingSchedule1,
                Room = room2,
                Username = "Marko",
                Password = "miki123",
                Name = "Marko",
                Surname = "Maric",
                Email = "Cajons@gmail.com",
                Appointments = appointmentList,
                Holidays = holidayList
            };
            var room3 = new Room
            {
                Id = Guid.NewGuid(),
                Type = RoomType.MEETING_ROOM
            };
            var doctorList = new List<Doctor>();
            doctorList.Add(doctor1);
            doctorList.Add(doctor2);
            var consilium = new Consilium(theme,doctorList,timeRange,room3);
            return consilium;
        }
        private Consilium SeedDataConsilium2(out Room room, out List<Doctor> doctorList)
        {
            Specialization specializationDermatology = new()
            {
                Id = Guid.NewGuid(),
                Name = "Dermatology"
            };
            var theme = "Theme";
         
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
            Room room2 = new()
            {
                Id = Guid.NewGuid()
            };
            var start = new DateTime(2022, 12, 17);
            var end = new DateTime(2022, 12, 19);
            var timeRange = new TimeRange
            {
                From = start,
                To = end,
                Duration = 60
            };
            var doctor1 = new Doctor
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
            var doctor2 = new Doctor
            {
                Id = Guid.NewGuid(),
                Specialization = specializationDermatology,
                Address = address,
                WorkingSchedule = workingSchedule1,
                Room = room2,
                Username = "Marko",
                Password = "miki123",
                Name = "Marko",
                Surname = "Maric",
                Email = "Cajons@gmail.com"
            };
            room = new Room
            {
                Id = Guid.NewGuid(),
                Type = RoomType.MEETING_ROOM
            };
            doctorList = new List<Doctor>();
            doctorList.Add(doctor1);
            doctorList.Add(doctor2);
            var consilium = new Consilium(theme,doctorList,timeRange,room);
            return consilium;
        }
    }
}