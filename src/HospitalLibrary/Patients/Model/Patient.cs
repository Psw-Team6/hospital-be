using System;
using System.Collections.Generic;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Patients.Model
{
    public class Patient : ApplicationUser
    {
        public Guid Id { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }

    }
}