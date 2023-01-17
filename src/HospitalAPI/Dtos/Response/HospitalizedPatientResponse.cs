using System;
using System.Collections.Generic;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class HospitalizedPatientResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jmbg{ get; set; }
        public string Gender { get; set; } 
        public string Phone { get; set; }
        public IEnumerable<HospitalizePatientAdmissionResponse> PatientAdmissions { get; set; }
    }
}