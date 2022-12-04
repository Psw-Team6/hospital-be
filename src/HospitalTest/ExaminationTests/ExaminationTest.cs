using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amqp.Types;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.CustomException;
using HospitalLibrary.Examinations.Exceptions;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Medicines.Model;
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
            var medicine = new Medicine
            {
                Name = "Brufen",
                Amount = 0,
            };
            var medicines = new List<Medicine> {medicine};
            var prescription = new ExaminationPrescription(medicines,"one gram per day");
            var prescriptions = new List<ExaminationPrescription> {prescription};
            //Act
            try
            {
                //Assert
                var examination = new Examination(appointment, "aaa", symptoms,prescriptions);
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
            var medicine = new Medicine
            {
                Id = default,
                Name = "Brufen",
                Amount = 0,
            };
            var medicines = new List<Medicine> {medicine};
            var prescription = new ExaminationPrescription(medicines,"one gram per day");
            var prescriptions = new List<ExaminationPrescription> {prescription};
            //Act
            try
            {
                //Assert
                var examination = new Examination(appointment, "aaa", symptoms,prescriptions);
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
            var medicine = new Medicine
            {
                Id = default,
                Name = "Brufen",
                Amount = 0,
            };
            var medicines = new List<Medicine> {medicine};
            var prescription = new ExaminationPrescription(medicines,"one gram per day");
            var prescriptions = new List<ExaminationPrescription> {prescription};
            //Act
            var examination = new Examination(appointment, "aaa", symptoms,prescriptions);
            //Assert
            examination.ShouldNotBeNull();
        }

    }
}