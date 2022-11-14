using System;
using System.Collections.Generic;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Medicines.Model
{
    public class Medicine
    {
        public Guid Id { get; set; }
        public  string Name { get; set; }
        public int Amount { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}