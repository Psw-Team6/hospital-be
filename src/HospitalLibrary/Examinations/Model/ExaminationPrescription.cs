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
        

        public IEnumerable<Medicine> _medicines;
        // public Examination Examination { get; set; }
        // public Guid ExaminationId { get; set; }
        // public IEnumerable<Guid> MedicineIds { get; set; }
        public IEnumerable<Medicine> Medicines
        {
            get => _medicines;
            set => _medicines = value;
        }
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