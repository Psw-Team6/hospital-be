using System;

namespace HospitalAPI.Dtos.Request
{
    public class WorkingScheduleRequest
    {
        public Guid Id { get; set; }
        public DateTime StartUpDate { get; set; }
        public DateTime ExpiresDate { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}