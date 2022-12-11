using System;
using System.Collections.Generic;
using HospitalLibrary.Examinations.Exceptions;
using HospitalLibrary.Medicines.Model;

namespace HospitalLibrary.Examinations.Model
{
    public class ExaminationPrescription
    {
        public Guid Id { get; set; }
        public string Usage { get; set; }

        public List<Medicine> Medicines { get; private set; }
        // public Examination Examination { get; set; }
        // public Guid ExaminationId { get; set; }

        private void AddMedicines(List<Medicine> medicines)
        {
            Medicines = medicines;
        }

        public ExaminationPrescription()
        {
        }

        public ExaminationPrescription(List<Medicine> medicines,string usage)
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