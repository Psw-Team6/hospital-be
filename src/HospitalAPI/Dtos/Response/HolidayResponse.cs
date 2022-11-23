using System;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class HolidayResponse
    {
        public Guid Id { get; set; }
        public bool IsUrgent { get; set; }
        public DateRange DateRange { get; set; }
        public Guid DoctorId { get; set; }
        public String Description { get; set; }
        public HolidayStatus HolidayStatus { get; set; }
    }
    
    