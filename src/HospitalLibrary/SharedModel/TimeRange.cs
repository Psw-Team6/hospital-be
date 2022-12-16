using System;
using HospitalLibrary.Common;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.SharedModel
{
    [Owned]
    public class TimeRange : ValueObject<TimeRange>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Duration { get; set; }
        
        public bool IsValidRange()
        {
            return From <= To;
        }
        public bool IsValidFrom()
        {
            return From.Date >=  DateTime.Now.Date;
        }
        public bool IsValidTo()
        {
            return To.Date >=  DateTime.Now.Date;
        }

        public bool IsValidDuration()
        {
            return Duration >= 30;
        }
        
        public bool IsBeforeAndTodayDate()
        {
            return From.Date.Date <= DateTime.Now.Date || To.Date.Date <= DateTime.Now.Date;
        }
        protected override bool EqualsCore(TimeRange other)
        {
            return From == other.From && To == other.To;
        }

        public DateTime AddDurationToDateTime(DateTime from)
        {
            return from.AddMinutes(Duration + 10);
        }

        protected override int GetHashCodeCore()
        {
            var hashCode = From.GetHashCode();
            hashCode = (hashCode * 397) ^ To.GetHashCode();
            return hashCode;
        }

        public DateTime SetStartTimeAndDate()
        {
           return From.Date + new TimeSpan(8, 0, 0);
        } 
        public DateTime SetFinishTimeAndDate()
        {
           return To.Date + new TimeSpan(22, 0, 0);
        }
    }
}