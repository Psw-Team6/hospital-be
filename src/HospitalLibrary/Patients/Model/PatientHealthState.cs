using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.Patients.Exceptions;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Patients.Model
{
    public class PatientHealthState:AbstractAggregate<Patient,Guid>
    {
        public BloodPressure BloodPressure { get;private set;}
        public BloodSugarLevel BloodSugarLevel { get; private set; }
        public int Weight { get;private set; }
        public DateTime SubmissionDate { get;private set; }
        public DateRange MenstrualCycle { get; private set; }
        public Percentage BodyFatPercent { get; private set; }
        public Guid RootId { get; private set; }

        public PatientHealthState(Patient patient,BloodPressure bloodPressure, BloodSugarLevel bloodSugarLevel, 
            int weight, DateTime submissionDate, DateRange menstrualCycle, Percentage bodyFatPercent)
        {
            BloodPressure = bloodPressure;
            BloodSugarLevel = bloodSugarLevel;
            Weight = weight;
            SubmissionDate = submissionDate;
            MenstrualCycle = menstrualCycle;
            BodyFatPercent = bodyFatPercent;
            Root = patient;
        }
        public void Validate()
        {
            if (Weight < 0)
            {
                throw new PatientException("Invalid weight");
            }
            if (Root.Gender == Gender.FEMALE && MenstrualCycle == null)
            {
                throw new PatientException("Menstrual cycle cannot be null");
            }
        }
        

        public List<string> CheckPatientState()
        {
            var states = HypertensionCheck().Concat(HypotensionCheck()).ToList();
            var levelSugar = BloodSugarLevelCheck();
            if (!string.IsNullOrEmpty(levelSugar))
            {
                states.Add(levelSugar);
            }

            return states;
        }

        private IEnumerable<string> HypotensionCheck()
        {
            var messages = new List<string>();
            switch (Root.Age)
            {
                case < 65:
                {
                    if (BloodPressure.LowerPressure < 60)
                    {
                        messages.Add( $"Diastolic blood pressure is low.Value: {BloodPressure.LowerPressure}.");
                    }

                    if (BloodPressure.UpperPressure < 90)
                    {
                        messages.Add($"Systolic blood pressure is low.Value: {BloodPressure.UpperPressure}.");
                    }

                    break;
                }
                case > 65:
                {
                    if (BloodPressure.LowerPressure < 50)
                    {
                        messages.Add( $"Diastolic blood pressure is  low.Value: {BloodPressure.LowerPressure}.");
                    }

                    if (BloodPressure.UpperPressure < 80)
                    {
                        messages.Add($"Systolic blood pressure is low.Value: {BloodPressure.UpperPressure}.");
                    }

                    break;
                }
            }

            return messages;
        }
        private IEnumerable<string> HypertensionCheck()
        {
            var messages = new List<string>();
            switch (Root.Age)
            {
                case < 65:
                {
                    if (BloodPressure.LowerPressure > 90)
                    {
                        messages.Add( $"Diastolic blood pressure is high.Value: {BloodPressure.LowerPressure}.");
                    }

                    if (BloodPressure.UpperPressure > 140)
                    {
                        messages.Add($"Systolic blood pressure is high.Value: {BloodPressure.UpperPressure}.");
                    }

                    break;
                }
                case > 65:
                {
                    if (BloodPressure.LowerPressure > 100)
                    {
                        messages.Add( $"Diastolic blood pressure is too low.Value: {BloodPressure.LowerPressure}.");
                    }

                    if (BloodPressure.UpperPressure > 150)
                    {
                        messages.Add($"Systolic blood pressure is too low.Value: {BloodPressure.UpperPressure}.");
                    }

                    break;
                }
            }

            return messages;
        }

        private string BloodSugarLevelCheck()
        {
            switch (Root.Age)
            {
                case < 65:
                {
                    if (BloodSugarLevel.SugarLevel > 180)
                    {
                        return $"Possible prediabetes state.Sugar level: {BloodSugarLevel.SugarLevel}.";
                    }
                    break;
                }
                case > 65:
                    if (BloodSugarLevel.SugarLevel > 200)
                    {
                        return $"Possible prediabetes state.Sugar level: {BloodSugarLevel.SugarLevel}.";
                    }
                    break;
            }

            return string.Empty;
        }

        public PatientHealthState(Patient root) : base(root)
        {
        }
        public PatientHealthState()
        {
        }
    }
}