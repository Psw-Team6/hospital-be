using System;
using HospitalLibrary.BloodUnits.Model;

namespace HospitalAPI.Dtos.Request
{
    public class BloodPrescriptionRequest
    {
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public Guid TreatmentReportId { get; set; }
    }
}