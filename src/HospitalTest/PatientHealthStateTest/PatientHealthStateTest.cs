using System;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.Patients.Exceptions;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.SharedModel;
using Xunit;

namespace HospitalTest.PatientHealthStateTest
{
    public class PatientHealthStateTests
    {
        [Fact]
        public void Validate_WeightLessThanZero_ThrowsPatientException()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "Milica",
                Surname = "Mitrovic",
                Gender = Gender.FEMALE,
                Age = 40
            };
            var healthState = new PatientHealthState(patient, new BloodPressure(70, 110)
                , new BloodSugarLevel(100), -1, new DateTime()
                , new DateRange(new DateTime(), new DateTime()), new Percentage(10));

            // Act & Assert
            Assert.Throws<PatientException>(() => healthState.Validate());
        }

        [Fact]
        public void Validate_FemalePatientWithoutMenstrualCycle_ThrowsPatientException()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "Milica",
                Surname = "Mitrovic",
                Gender = Gender.FEMALE,
                Age = 40
            };
            var healthState = new PatientHealthState(patient, new BloodPressure(70, 110)
                , new BloodSugarLevel(100), 70, new DateTime()
                , null, new Percentage(10));
            // Act & Assert
            Assert.Throws<PatientException>(() => healthState.Validate());
        }

        [Fact]
        public void CheckPatientState_PatientHasHypertension_ReturnsExpectedMessages()
        {
            // Arrange
            // Arrange
            var patient = new Patient
            {
                Name = "Milica",
                Surname = "Mitrovic",
                Gender = Gender.FEMALE,
                Age = 40
            };
            var healthState = new PatientHealthState(patient, new BloodPressure(91, 141)
                , new BloodSugarLevel(100), 70, new DateTime()
                , new DateRange(new DateTime(), new DateTime()), new Percentage(10));

            // Act
            var messages = healthState.CheckPatientState();

            // Assert
            Assert.Contains("Diastolic blood pressure is high.Value: 91.", messages);
            Assert.Contains("Systolic blood pressure is high.Value: 141.", messages);
        }

        [Fact]
        public void CheckPatientState_PatientHasHypotension_ReturnsExpectedMessages()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "Milica",
                Surname = "Mitrovic",
                Gender = Gender.FEMALE,
                Age = 40
            };
            var healthState = new PatientHealthState(patient, new BloodPressure(50, 70)
                , new BloodSugarLevel(100), 70, new DateTime()
                , new DateRange(new DateTime(), new DateTime()), new Percentage(10));

            // Act
            var messages = healthState.CheckPatientState();

            // Assert
            Assert.Contains("Diastolic blood pressure is low.Value: 50.", messages);
            Assert.Contains("Systolic blood pressure is low.Value: 70.", messages);
        }

        [Fact]
        public void CheckPatientState_PatientHasHighBloodSugarLevel_ReturnsExpectedMessage()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "Milica",
                Surname = "Mitrovic",
                Gender = Gender.FEMALE,
                Age = 40
            };
            var healthState = new PatientHealthState(patient, new BloodPressure(80, 120)
                , new BloodSugarLevel(200), 70, new DateTime()
                , new DateRange(new DateTime(), new DateTime()), new Percentage(10));
            // Act
            var messages = healthState.CheckPatientState();

            // Assert
            Assert.Contains("Possible prediabetes state.Sugar level: 200.", messages);
        }
    }
}