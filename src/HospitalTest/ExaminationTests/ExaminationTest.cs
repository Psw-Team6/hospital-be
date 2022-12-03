using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.CustomException;
using HospitalLibrary.Examinations.Exceptions;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.SharedModel;
using Shouldly;
using Xunit;

namespace HospitalTest.ExaminationTests
{
    public class ExaminationTest
    {
        [Fact]
        public void Invalid_examination_date()
        {
            //Arrange
            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
                Emergent = false,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = new DateTime(2023, 10, 27, 15, 0, 0),
                    To = new DateTime(2023, 10, 27, 15, 15, 0)
                },
            };
            Symptom symptom = new()
            {
                Id = new Guid(),
                Description = "Polen"
            };
            var symptoms = new List<Symptom> {symptom};
            //Act
            try
            {
                //Assert
                var examination = new Examination(appointment, "aaa", symptoms);
            }
            catch (ExaminationInvalidDate e)
            {
                Assert.Equal(e.Message, Examination.InvalidDateMessage);
            }

        }
        [Fact]
        public void Invalid_examination_appointment_state()
        {
            //Arrange
            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
                Emergent = false,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Finished,
                Duration = new DateRange
                {
                    From = DateTime.Now,
                    To = DateTime.Now.AddMinutes(30)
                },
            };
            Symptom symptom = new()
            {
                Id = new Guid(),
                Description = "Polen"
            };
            var symptoms = new List<Symptom> {symptom};
            //Act
            try
            {
                //Assert
                var examination = new Examination(appointment, "aaa", symptoms);
            }
            catch (AppointmentExaminationInvalidState e)
            {
                Assert.Equal(e.Message, Examination.InvalidAppointmentStateMessage);
            }

        }

        [Fact]
        public void Examination_success_creation()
        {
            //Arrange
            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
                Emergent = false,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = DateTime.Now,
                    To = DateTime.Now.AddMinutes(30)
                },
            };
            Symptom symptom = new()
            {
                Id = new Guid(),
                Description = "Polen"
            };
            var symptoms = new List<Symptom> {symptom};
            //Act
            var examination = new Examination(appointment, "aaa", symptoms);
            //Assert
            examination.ShouldNotBeNull();
        }

    }
}