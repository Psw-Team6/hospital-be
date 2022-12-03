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
        
        public bool IsBeforeAndTodayDate()
        {
            return From.Date.Date <= DateTime.Now.Date || To.Date.Date <= DateTime.Now.Date;
        }
        
        public bool CheckAfterMonthDate()
        {
            return From.Date.Date >= DateTime.Now.Date.AddMonths(1) && To.Date.Date >= DateTime.Now.Date.AddMonths(1);
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