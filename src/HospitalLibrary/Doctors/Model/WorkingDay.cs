using System;
using Npgsql.TypeHandlers.DateTimeHandlers;

namespace HospitalLibrary.Doctors.Model
{
    public class WorkingDay
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        
    }
}