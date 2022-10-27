using System;
using System.Collections.Generic;
using HospitalLibrary.sharedModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HospitalLibrary.Doctors.Model
{
    public class WorkingSchedule
    {
        public Guid Id { get; set; }
        public DateTime StartUpDate { get; set; }
        public DateTime ExpiresDate { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
      //  public List<Doctor> Doctors { get; set; }

        public bool IsExpired()
        {
            return ExpiresDate < DateTime.Now;
        }
    }
}