﻿using System;
using  HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Model;
namespace HospitalLibrary.Prescriptions.Model
{
    public class MedicinePrescription
    {
        public Guid Id { get; set; }
        public Medicine Medicine { get; set; }
        public Guid MedicineId { get; set; }
        public Patient Patient { get; set; }
        public Guid PatientId { get; set; }
        public string Description { get; set; }
    }
}