using System;
using System.Collections.Generic;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.sharedModel;
using HospitalLibrary.Doctors.Model;

namespace HospitalAPI.Dtos.Response
{
    public class PatientProfileResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public Doctor Doctor { get; set; }
        public List<Allergen> Allergies { get; set; }
        public BloodType BloodType { get; set; }
    }
}