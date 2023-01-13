using System;
using HospitalLibrary.Enums;

namespace HospitalAPI.Dtos.Request
{
    public class FeedbackRequest
    {
        public Guid PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsPublic { get; set; }
        public Status Status{ get; set; }
    }
}