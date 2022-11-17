using System;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.sharedModel
{
    [Owned]
    public class DateRange
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public bool IsValidRange()
        {
            return From < To;
        }
        
        public bool IsBeforeDate()
        {
            return From.Date.Date <= DateTime.Now.Date || To.Date.Date <= DateTime.Now.Date;
        }
        
        public bool CheckAfterMonthDate()
        {
            return From.Date.Date >= DateTime.Now.Date.AddMonths(1) && To.Date.Date >= DateTime.Now.Date.AddMonths(1);
        }
    }
}