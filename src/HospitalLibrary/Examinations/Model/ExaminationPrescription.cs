﻿using System;
using System.Collections.Generic;
using HospitalLibrary.Examinations.Exceptions;
using HospitalLibrary.Medicines.Model;

namespace HospitalLibrary.Examinations.Model
{
    public class ExaminationPrescription
    {
        public Guid Id { get; set; }
        public string Usage { get; set; }
        

        public IEnumerable<Medicine> Medicines { get; private set; }
        private void AddMedicines(IEnumerable<Medicine> medicines)
        {
            Medicines = medicines;
        }

        public ExaminationPrescription()
        {
        }

        public ExaminationPrescription(IEnumerable<Medicine> medicines,string usage)
        {
            AddMedicines(medicines);
            Usage = usage;
        }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Usage);
        }
    }
}