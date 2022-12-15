using System;
using System.Diagnostics.CodeAnalysis;
using HospitalLibrary.Common;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.SharedModel
{
    [Owned]
    public class NullableDateRange : ValueObject<NullableDateRange>
    {
        public DateTime From { get; set; }
        
        public DateTime? To { get; set; }
        
        public bool IsValidRange()
        {
            if (To != null)
            {
                return From < To;
            }

            return true;
        }

        public bool IsBeforeAndTodayDate()
        {
            if (To != null)
            {
                return From.Date.Date <= DateTime.Now.Date || To?.Date.Date <= DateTime.Now.Date;

            }
            else
            {
                return From.Date.Date <= DateTime.Now.Date;
            }
            
        }
        protected override bool EqualsCore(NullableDateRange other)
        {
            return From == other.From && To == other.To;
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
        public DateTime? SetFinishTimeAndDate()
        {
            return To?.Date + new TimeSpan(22, 0, 0);
        }
    }
}