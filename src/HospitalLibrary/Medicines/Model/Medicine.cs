using System;
using System.Collections.Generic;
using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Medicines.Model
{
    public class Medicine
    {
        public Guid Id { get; set; }
        public  string Name { get; set; }
        public int Amount { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public List<Allergen> Allergens { get; set; }

        public List<MedicinePrescription> MedicinePrescription { get; set; }
    }
}