using System;
using HospitalLibrary.Common;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.SharedModel
{
    [Owned]
    public class DateRange:ValueObject<DateRange>
    {
        
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public bool IsValidRange()
        {
            return From < To;
        }

        public DateRange(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateRange()
        {
        }

        public bool IsBeforeAndTodayDate()
        {
            return From.Date.Date <= DateTime.Now.Date || To.Date.Date <= DateTime.Now.Date;
        }
        
        public bool CheckAfterMonthDate()
        {
            return From.Date.Date >= DateTime.Now.Date.AddDays(3) && To.Date.Date >= DateTime.Now.Date.AddDays(3);
        }

        protected override bool EqualsCore(DateRange other)
        {
            return From == other.From && To == other.To;
        }

        protected override int GetHashCodeCore()
        {
            var hashCode = From.GetHashCode();
            hashCode = (hashCode * 397) ^ To.GetHashCode();
            return hashCode;
        }
    }
}