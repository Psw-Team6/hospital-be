using System;

namespace HospitalLibrary.sharedModel
{
    public class TimeSlot
    {
        public Guid Id { get; set; }
        
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }

        public TimeSlot(Guid id, DateTime startTime, TimeSpan duration)
        {
            Id = id;
            
            StartTime = startTime;
            Duration = duration;
        }
    }
}