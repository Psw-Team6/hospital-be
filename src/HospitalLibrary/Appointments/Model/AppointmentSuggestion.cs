using System;
using System.Collections;
using System.Collections.Generic;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Appointments.Model
{
    public class AppointmentSuggestion
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateRange Duration { get; set; }
    }
}