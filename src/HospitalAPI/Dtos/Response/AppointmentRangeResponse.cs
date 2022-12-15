using System;
using System.Collections;
using System.Collections.Generic;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class AppointmentRangeResponse
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateRange Duration { get; set; }
    }
}