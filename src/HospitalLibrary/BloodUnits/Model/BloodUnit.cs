using System;

namespace HospitalLibrary.BloodUnits.Model
{
    public class BloodUnit
    {
        public Guid Id { get; set; }
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public String BloodBankName { get; set; }
    }
}