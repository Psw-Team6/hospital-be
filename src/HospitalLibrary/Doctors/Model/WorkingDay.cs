using System;
using Npgsql.TypeHandlers.DateTimeHandlers;

namespace HospitalLibrary.Doctors.Model
{
    public class WorkingDay
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public bool IsHoliday { get; set;}
        public bool IsSunday { get; set;}
        public bool IsSaturday { get; set;}
    }
}