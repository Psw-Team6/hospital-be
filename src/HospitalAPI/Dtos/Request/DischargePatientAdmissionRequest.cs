using System;

namespace HospitalAPI.Dtos.Request
{
    public class DischargePatientAdmissionRequest
    {
        public Guid Id { get; set; }
        public string ReasonOfDischarge { get; set; }
    }
}