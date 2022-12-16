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
        public static readonly object[][] WrongDates =
        {
            new object[] { new DateTime(2023, 10, 27, 15, 0, 0), new DateTime(2023, 10, 27, 15, 15, 0)},
            new object[] { new DateTime(2017, 2, 1), new DateTime(2018, 2, 28)},
            new object[] { DateTime.Now.AddMinutes(130), DateTime.Now.AddMinutes(160)},
            new object[] { DateTime.Now.AddMinutes(1000), DateTime.Now.AddMinutes(1020)},
            new object[] { DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-3).AddMinutes(30)},
            new object[] { DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-4).AddMinutes(-30)},
        };

        [Theory,MemberData(nameof(WrongDates))]
        public void Invalid_examination_date(DateTime startDate, DateTime endDate)
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
                    From = startDate,
                    To = endDate
                }
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
            var examination = new Examination(appointment, "aaa", symptoms,prescriptions);
            //Act
            try
            {
                examination.ValidateExamination();
            }
            catch (ExaminationInvalidDate e)
            {
                //Assert
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
                    From = DateTime.Now.AddMinutes(-60),
                    To = DateTime.Now.AddMinutes(-30)
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
            try
            {
                //Assert
                examination.ValidateExamination();
            }
            catch (AppointmentExaminationInvalidState e)
            {
                Assert.Equal(e.Message, Examination.InvalidAppointmentStateMessage);
            }

        }
        [Fact]
        public void Examination_invalid_prescriptions()
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
                    From = DateTime.Now.AddMinutes(-60),
                    To = DateTime.Now.AddMinutes(-30)
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
            var prescription = new ExaminationPrescription(medicines,"");
            var prescriptions = new List<ExaminationPrescription> {prescription};
            //Act
            var examination = new Examination(appointment, "aaa", symptoms,prescriptions);
            try
            {
                examination.ValidateExamination();
                //Assert
            }
            catch (ExaminationPrescriptionException e)
            {
                Assert.Equal(e.Message, Examination.InvalidPrescriptionsMessage);
            }
        }
        [Fact]
        public void Examination_invalid_anamnesis()
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
                    From = DateTime.Now.AddMinutes(-60),
                    To = DateTime.Now.AddMinutes(-30)
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
            var prescription = new ExaminationPrescription(medicines,"once per day");
            var prescriptions = new List<ExaminationPrescription> {prescription};
            //Act
            var examination = new Examination(appointment, "", symptoms,prescriptions);
            try
            {
                //Assert
                examination.ValidateExamination();
            }
            catch (ExaminationInvalidAnamnesis e)
            {
                Assert.Equal(e.Message, Examination.InvalidAnamnesisMessage);
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
                    From = DateTime.Now.AddMinutes(-60),
                    To = DateTime.Now.AddMinutes(-30)
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
            examination.ValidateExamination();
            //Assert
            examination.ShouldNotBeNull();
        }

    }
}