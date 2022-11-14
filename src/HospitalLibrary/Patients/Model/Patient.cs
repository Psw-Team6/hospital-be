using System;
using System.Collections;
using System.Collections.Generic;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.sharedModel;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalLibrary.Patients.Model
{
    public class Patient : ApplicationUser
    {
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
        public IEnumerable<Ingredient> Allergies { get; set; }
        public IEnumerable<PatientAdmission> PatientAdmissions { get; set; }
        public IEnumerable<TreatmentReport> TreatmentReports { get; set; }

    }
}