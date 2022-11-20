using System;
using HospitalLibrary.Enums;

namespace HospitalAPI.Dtos.Response
{
    public class PatientAdmissionResponse
    {
        public Guid Id { get; set; }
        public PatientResponseName Patient { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public String Reason { get; set; }
    }
}