using System;

namespace HospitalAPI.Dtos.Response
{
    public class HospitalizePatientAdmissionResponse
    {
        public Guid Id { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public string Reason { get; set; }
        public DateTime? DateOfDischarge { get; set; }
        public string ReasonOfDischarge { get; set; }
    }
}