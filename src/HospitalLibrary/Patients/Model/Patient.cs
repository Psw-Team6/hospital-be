using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.ApplicationUsers;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.sharedModel;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalLibrary.Patients.Model
{
    [Table("Patients")]
    public class Patient : ApplicationUser
    {
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
        public IEnumerable<Ingredient> Allergies { get; set; }
        public IEnumerable<PatientAdmission> PatientAdmissions { get; set; }
        public IEnumerable<TreatmentReport> TreatmentReports { get; set; }

        public Gender Gender { get; set; }
        public int Age { get; set; }

    }
}