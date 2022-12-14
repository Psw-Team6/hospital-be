using System;
using System.Collections.Generic;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Medicines.Model
{
    public class Medicine
    {
        public Guid Id { get; set; }
        public  string Name { get; set; }
        public int Amount { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Invalid medicine name");
            }
        }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public List<ExaminationPrescription> ExaminationPrescriptions { get; private set; }
        public List<MedicinePrescription> MedicinePrescription { get;private set; }
    }
}