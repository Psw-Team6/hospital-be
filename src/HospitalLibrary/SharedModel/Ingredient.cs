using System;
using System.Collections.Generic;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.SharedModel
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Medicine> Medicines { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        
    }
}