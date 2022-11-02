using System;
using HospitalLibrary.Enums;
using HospitalLibrary.Patients.Model;

namespace HospitalAPI.Dtos.Response
{
    public class FeedbackResponse
    {
        public Guid Id { get; set; }
        public PatientResponseName Patient { get; set; }
        public DateTime Date { get; set; }
        public String Text { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsPublic { get; set; }
        public Status Status { get; set; }
    }
}