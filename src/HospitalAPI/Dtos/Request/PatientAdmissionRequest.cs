using System;

namespace HospitalAPI.Dtos.Request
{
    public class PatientAdmissionRequest
    {
        public Guid PatientId { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public String Reason { get; set; }
    }
}