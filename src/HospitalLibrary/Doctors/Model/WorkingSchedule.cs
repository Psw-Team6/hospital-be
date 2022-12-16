using System;
using System.Collections.Generic;
using HospitalLibrary.SharedModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HospitalLibrary.Doctors.Model
{
    public class WorkingSchedule
    {
        public Guid Id { get; set; }
        public NullableDateRange ExpirationDate { get; set; } 
        public DateRange DayOfWork { get; set; }
      //  public List<Doctor> Doctors { get; set; }
        

        public bool IsExpired()
        {
            return ExpirationDate.To > DateTime.Now;
        }
    }
}