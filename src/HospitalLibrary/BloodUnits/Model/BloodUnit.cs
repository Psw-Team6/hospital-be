using System;
using System.Collections.Generic;
using HospitalLibrary.BloodConsumptions.Model;

namespace HospitalLibrary.BloodUnits.Model
{
    public class BloodUnit
    {
        public Guid Id { get; set; }
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public String BloodBankName { get; set; }
        public IEnumerable<BloodConsumption> Consumptions { get; set; }

        public void decreseAmount(int consumptionAmount)
        {
            Amount -=consumptionAmount;
        }
    }
}