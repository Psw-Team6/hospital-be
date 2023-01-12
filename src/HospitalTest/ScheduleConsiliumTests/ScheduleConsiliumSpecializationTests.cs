using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amqp.Types;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Consiliums.Service;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Enums;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;

namespace HospitalTest.ScheduleConsiliumTests
{
    public class ScheduleConsiliumSpecializationTests
    {
        [Fact]
        public void Schedule_Consilium_Specialization_No_Appointment()
        {
            var dictionary = new Dictionary<DateTime, List<Doctor>>();
            var mockService = new Mock<IConsiliumService>();
            var specializations = SeedSpecialization(out var doctor);
            var consilium = SeedDataConsilium2(out var room, out var doctorList);
            mockService.Setup(s =>
                    s.RemoveAppointmentsWithNoDoctors(dictionary,specializations,doctor))
                .Returns(() => dictionary);
            
            // Act
            var service = mockService.Object;
            try
            {
                var result = service.FindTimeRangeForAllDoctors(new Consilium());
            }
            catch (ConsiliumException e)
            {
                Assert.Equal("The system couldn't schedule consilium in that time range" ,e.Message);
            }
        }
        
        [Fact]
        public void Schedule_Consilium_Specialization_Has_Appointments()
        {
            var dictionary = new Dictionary<DateTime, List<Doctor>>();
            var mockService = new Mock<IConsiliumService>();
            var specializations = SeedSpecialization(out var doctor);
            var list = new List<Doctor>();
            list.Add(doctor);
            dictionary.Add(new DateTime(),list);
            var consilium = SeedDataConsilium7(out var room, out  list);
            mockService.Setup(s =>
                    s.RemoveAppointmentsWithNoDoctors(dictionary,specializations,doctor))
                .Returns(() => dictionary);
            
            var service = mockService.Object;
            
            var result = service.RemoveAppointmentsWithNoDoctors(dictionary,specializations,doctor);
            
            Assert.True(result.Count >0 );
        }

        private List<Specialization> SeedSpecialization(out Doctor o)
        {
            Specialization specializationDermatology = new()
            {
                Id = Guid.NewGuid(),
                Name = "Dermatology"
            };
            Specialization specializationGeneral = new()
            {
                Id = Guid.NewGuid(),
                Name = "General"
            };
            var specializations = new List<Specialization>();
            specializations.Add(specializationDermatology);
            specializations.Add(specializationGeneral);
            o = new Doctor
            {
                Id = Guid.NewGuid(),
                Specialization = specializationDermatology,
                Address = default,
                WorkingSchedule = default,
                Room = default,
                Username = "Ilija",
                Password = "miki123",
                Name = "Ilija",
                Surname = "Maric",
                Email = "Cajons@gmail.com"
            };
            return specializations;
        }

        private Consilium SeedDataConsilium7(out Room room, out List<Doctor> doctorList)
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
        
       
        [Fact]
        public async Task Schedule_Consilium_Meeting_Room_Found()
        {
                // Arrange
                var dictionary = new Dictionary<DateTime, List<Doctor>>();
                var mockService = new Mock<IConsiliumService>();
                var specializations = SeedSpecialization(out var doctor);
                var list = new List<Doctor>();
                list.Add(doctor);
                dictionary.Add(new DateTime(),list);
                var consilium = SeedDataConsilium7(out var room, out var doctorList);
                mockService.Setup(s =>
                        s.RemoveAppointmentsWithNoDoctors(dictionary,specializations,doctor))
                    .Returns(() => dictionary);

                var start = new DateTime(2022, 12, 17);
                var end = new DateTime(2022, 12, 19);
                var timeRange = new TimeRange
                {
                    From = start,
                    To = end,
                    Duration = 60
                };
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
            var dictionary = new Dictionary<DateTime, List<Doctor>>();
            var mockService = new Mock<IConsiliumService>();
            var specializations = SeedSpecialization(out var doctor);
            var list = new List<Doctor>();
            list.Add(doctor);
            dictionary.Add(new DateTime(),list);
            var consilium = SeedDataConsilium7(out var room, out var doctorList);
            mockService.Setup(s =>
                    s.RemoveAppointmentsWithNoDoctors(dictionary,specializations,doctor))
                .Returns(() => dictionary);

            var start = new DateTime(2022, 12, 17);
            var end = new DateTime(2022, 12, 19);
            var timeRange = new TimeRange
            {
                From = start,
                To = end,
                Duration = 60
            };
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
            var dictionary = new Dictionary<DateTime, List<Doctor>>();
            var mockService = new Mock<IConsiliumService>();
            var specializations = SeedSpecialization(out var doctor);
            var list = new List<Doctor>();
            list.Add(doctor);
            dictionary.Add(new DateTime(),list);
            var consilium = SeedDataConsilium7(out var room, out var doctorList);
            mockService.Setup(s =>
                    s.RemoveAppointmentsWithNoDoctors(dictionary,specializations,doctor))
                .Returns(() => dictionary);

            var start = new DateTime(2022, 12, 17);
            var end = new DateTime(2022, 12, 19);
            var timeRange = new TimeRange
            {
                From = start,
                To = end,
                Duration = 60
            };
            mockService.Setup(s =>
                    s.FindAvailableMeetingRoom(timeRange))
                .ReturnsAsync(() => room);
            mockService.Setup(s =>
                    s.ScheduleConsiliumSpecialization(consilium,specializations,doctor.Id))
                .ReturnsAsync(() => consilium);
            var service = mockService.Object;
            var result = await service.ScheduleConsiliumSpecialization(consilium,specializations,doctor.Id);
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