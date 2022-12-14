using System;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Holidays.Model
{
    [Table ("Holidays")]
    public class Holiday
    {
        public Guid Id { get; set; }
        public Doctor Doctor { get; set; }
        
        public Guid DoctorId { get; set; }
        public DateRange DateRange { get; set;}
        
        public String Description { get; set; }
        
        public Boolean IsUrgent { get; set; }
        
        public HolidayStatus HolidayStatus { get; set; }
    }
}