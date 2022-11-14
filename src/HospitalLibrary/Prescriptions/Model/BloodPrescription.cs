using System;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Prescriptions.Model
{
    public class BloodPrescription
    {
        public Guid Id { get; set; }
        // public Blood Blood { get; set; }
        // public Guid BloodId { get; set; }
        public Patient Patient { get; set; }
        public Guid PatientId { get; set; }
        public string Description { get; set; }
   
    }
}