using System;
using System.Collections.Generic;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class PatientRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public List<String> AllergyIds { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg { get; set; }
        public string Phone{ get; set; }
        public string DoctorId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public Gender Gender { get; set; }
    }
}