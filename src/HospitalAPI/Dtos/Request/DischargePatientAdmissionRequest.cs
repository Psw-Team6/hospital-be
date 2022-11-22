using System;

namespace HospitalAPI.Dtos.Request
{
    public class DischargePatientAdmissionRequest
    {
        public Guid Id { get; set; }
        public String ReasonOfDischarge { get; set; }
    }
}