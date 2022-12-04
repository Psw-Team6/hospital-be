using System;
using System.Collections.Generic;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.SharedModel
{
    public class Allergen
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        
    }
}