using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Repository;
using HospitalLibrary.sharedModel;
using Moq;
using Shouldly;
using Xunit;

namespace HospitalTest.ScheduleTest
{
    public class ScheduleAppointmentTest
    {

        [Fact]
        public async Task Schedule_appointment()
        {
            var mockGeneric = new Mock<DoctorRepository>();
            var stubUnit = new Mock<IUnitOfWork>();
            var stubEmailRepo = new Mock<IEmailService>();
            var mockPatient = new Mock<IPatientRepository>();

            var apps = new List<Appointment>();
            var doctors = new List<Doctor>();
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
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Prvi",
                Number = "11A",
                PositionX = 0,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
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
            Patient patient1 = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Sale",
                Password = "sale1312",
                Name = "Sale",
                Surname = "Lave",
                Email = "psw.isa.mail@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222"
            };
            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
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
            stubUnit.Setup(x => x.PatientRepository).Returns(mockPatient.Object);
            //stubUnit.Setup(x => x.GetRepository<DoctorRepository>()).Returns(mockGeneric.Object);
            stubUnit.Setup(x => x.PatientRepository.GetByIdAsync(patient1.Id))
                .ReturnsAsync(patient1);
            var scheduleService = new ScheduleService(stubUnit.Object,stubEmailRepo.Object);
            Func<Task> act = () => scheduleService.ScheduleAppointment(appointment);
            var ex = await Assert.ThrowsAsync<DoctorNotExist>(act);
            Assert.Equal("Doctor does not exist",ex.Message);
        }
    }
}