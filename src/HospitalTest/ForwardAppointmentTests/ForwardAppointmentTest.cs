using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Doctors.Service;
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
                To = new DateTime(2023, 10, 29, 15, 15, 0)
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
    }
}