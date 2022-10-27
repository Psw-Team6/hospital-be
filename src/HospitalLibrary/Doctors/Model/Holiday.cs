using System;

namespace HospitalLibrary.Doctors.Model
{
    public class Holiday
    {
        public Guid Id { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}