using System;
using System.Collections;
using System.Collections.Generic;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class AppointmentRangeResponse
    {
        public Guid DoctorId;
        public Guid PatientId;
        public DateRange Range;
    }
}