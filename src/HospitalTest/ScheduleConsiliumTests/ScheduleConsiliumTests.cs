using System;
using System.Collections.Generic;
using HospitalLibrary.Common;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Consiliums.Repository;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;

namespace HospitalTest.ScheduleConsiliumTests
{
    public class ScheduleConsiliumTests
    {
        [Fact]
        public void Schedule_Consilium_Invalid_Theme()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockConsilium = new Mock<IConsiliumRepository>();
            var consilium = SeedDataConsilium();
            mockUnitOfWork.Setup(uw => uw.ConsiliumRepository).Returns(mockConsilium.Object);
            try
            {
                //Assert
                consilium.ValidateConsilium();
            }
            catch (ThemeNotExist e)
            {
                Assert.Equal("You must fill out theme!" ,e.Message);
            }
        }
        [Fact]
        public void Schedule_Consilium_Invalid_Time_Range_From_After_To()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockConsilium = new Mock<IConsiliumRepository>();
            var consilium = SeedDataConsilium2();
            mockUnitOfWork.Setup(uw => uw.ConsiliumRepository).Returns(mockConsilium.Object);
            try
            {
                //Assert
                consilium.ValidateConsilium();
            }
            catch (TimeRangeException e)
            {
                Assert.Equal("Dates are invalid!" ,e.Message);
            }
        }   
        [Fact]
        public void Schedule_Consilium_Invalid_Time_Range_From_Before_Now()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockConsilium = new Mock<IConsiliumRepository>();
            var consilium = SeedDataConsilium3();
            mockUnitOfWork.Setup(uw => uw.ConsiliumRepository).Returns(mockConsilium.Object);
            try
            {
                //Assert
                consilium.ValidateConsilium();
            }
            catch (TimeRangeException e)
            {
                Assert.Equal("Start date is invalid!" ,e.Message);
            }
        }  
        [Fact]
        public void Schedule_Consilium_No_Doctors()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockConsilium = new Mock<IConsiliumRepository>();
            var consilium = SeedDataConsilium5();
            mockUnitOfWork.Setup(uw => uw.ConsiliumRepository).Returns(mockConsilium.Object);
            try
            {
                //Assert
                consilium.ValidateConsilium();
            }
            catch (ConsiliumDoctorsNotExist e)
            {
                Assert.Equal("There are no doctors!" ,e.Message);
            }
        }  
        [Fact]
        public void Schedule_Consilium_Invalid_Time_Range_To_Before_Now()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockConsilium = new Mock<IConsiliumRepository>();
            var consilium = SeedDataConsilium4();
            mockUnitOfWork.Setup(uw => uw.ConsiliumRepository).Returns(mockConsilium.Object);
            try
            {
                //Assert
                consilium.ValidateConsilium();
            }
            catch (TimeRangeException e)
            {
                Assert.Equal("End date is invalid!" ,e.Message);
            }
        }

        private Consilium SeedDataConsilium()
        {
            Specialization specializationDermatology = new()
            {
                Id = Guid.NewGuid(),
                Name = "Dermatology"
            };
            var theme = "";
            var timeRange = new TimeRange
            {
                From = new DateTime(),
                To = new DateTime(),
                Duration = 10
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
            var doctorList = new List<Doctor>();
            doctorList.Add(doctor1);
            var consilium = new Consilium(theme,doctorList,timeRange);
            return consilium;
        }
        private Consilium SeedDataConsilium2()
        {
            var start = new DateTime(2022, 12, 30);
            var end = new DateTime(2022, 12, 29);
            Specialization specializationDermatology = new()
            {
                Id = Guid.NewGuid(),
                Name = "Dermatology"
            };
            var theme = "Theme";
            var timeRange = new TimeRange
            {
                From = start,
                To = end,
                Duration = 10
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
            };
            var doctorList = new List<Doctor>();
            doctorList.Add(doctor1);
            var consilium = new Consilium(theme,doctorList,timeRange);
            return consilium;
        }
        private Consilium SeedDataConsilium3()
        {
            var start = new DateTime(2022, 12, 14);
            var end = new DateTime(2022, 12, 17);
            Specialization specializationDermatology = new()
            {
                Id = Guid.NewGuid(),
                Name = "Dermatology"
            };
            var theme = "Theme";
            var timeRange = new TimeRange
            {
                From = start,
                To = end,
                Duration = 10
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
            var doctorList = new List<Doctor>();
            doctorList.Add(doctor1);
            var consilium = new Consilium(theme,doctorList,timeRange);
            return consilium;
        }  
        private Consilium SeedDataConsilium4()
        {
            var start = new DateTime(2022, 12, 17);
            var end = new DateTime(2022, 12, 14);
            Specialization specializationDermatology = new()
            {
                Id = Guid.NewGuid(),
                Name = "Dermatology"
            };
            var theme = "Theme";
            var timeRange = new TimeRange
            {
                From = start,
                To = end,
                Duration = 10
            };
            WorkingSchedule workingSchedule1 = new()
            {
                Id = Guid.NewGuid(),
                ExpirationDate = new NullableDateRange()
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
            };
            var doctorList = new List<Doctor>();
            doctorList.Add(doctor1);
            var consilium = new Consilium(theme,doctorList,timeRange);
            return consilium;
        }
        private Consilium SeedDataConsilium5()
        {
            var start = new DateTime(2022, 12, 17);
            var end = new DateTime(2022, 12, 19);
           
            var theme = "Theme";
            var timeRange = new TimeRange
            {
                From = start,
                To = end,
                Duration = 60
            };
          
            var doctorList = new List<Doctor>();
            if (doctorList == null) throw new ArgumentNullException(nameof(doctorList));
            var consilium = new Consilium(theme,doctorList,timeRange);
            return consilium;
        }
    }
}