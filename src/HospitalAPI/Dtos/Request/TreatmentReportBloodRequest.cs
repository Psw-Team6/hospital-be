using System;
using HospitalLibrary.Prescriptions.Model;

namespace HospitalAPI.Dtos.Request
{
    public class TreatmentReportBloodRequest
    {
        public Guid PatientAdmissionId { get; set; }
        
        public BloodPrescriptionRequest BloodPrescriptionRequest { get; set; } 
    }
}