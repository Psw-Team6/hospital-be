using System;
using HospitalLibrary.Enums;

namespace HospitalAPI.Dtos.Response
{
    public class FeedbackStatusResponse
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime Date { get; set; }
        public String Text { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsPublic { get; set; }
        public Status Status { get; set; }
    }
}