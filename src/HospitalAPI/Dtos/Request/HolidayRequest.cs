using System;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class HolidayRequest
    {
        public bool IsUrgent { get; set; }
        public DateRange DateRange { get; set; }
        public Guid DoctorId { get; set; }
        public String Description { get; set; }
        public HolidayStatus HolidayStatus { get; set; }

    }
}