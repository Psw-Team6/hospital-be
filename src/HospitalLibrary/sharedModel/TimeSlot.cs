using System;

namespace HospitalLibrary.sharedModel
{
    public class TimeSlot
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}