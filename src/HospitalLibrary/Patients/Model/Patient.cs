﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Patients.Model
{
    [Table("Patients")]
    public class Patient : ApplicationUser,IAggregateRoot<Guid>
    {
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
        public List<Allergen> Allergies { get; set; }
        public IEnumerable<PatientAdmission> PatientAdmissions { get; set; }
       // public IEnumerable<TreatmentReport> TreatmentReports { get; set; }
        public IEnumerable<string> AllergyIds;

        public Gender Gender { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Doctor Doctor { get; set; }
        public Guid DoctorId { get; set; }
        public BloodType? BloodType { get; set; }

        public void CalculateAge()
        {
            DateTime today = DateTime.Now;
            Age = DateOfBirth.Year - today.Year;
        }
        

    }
}