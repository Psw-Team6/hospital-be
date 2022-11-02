using System;
using System.Collections.Generic;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Patients.Model
{
    public class Patient : ApplicationUser
    {
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }

    }
}