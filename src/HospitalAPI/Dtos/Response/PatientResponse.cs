﻿using System;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class PatientResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg{ get; set; }
        public string Phone{ get; set; }
        public Gender Gender{ get; set; }
    }
}