using System;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class PatientHealthStateDto
    {
        public BloodPressure BloodPressure { get; set;}
        public BloodSugarLevel BloodSugarLevel { get; set; }
        public int Weight { get; set; }
        public Guid PatientId { get; set;}
        public DateTime SubmissionDate { get; set; }
        public DateRange MenstrualCycle { get;  set; }
        public Percentage BodyFatPercent { get;  set; }
    }
}