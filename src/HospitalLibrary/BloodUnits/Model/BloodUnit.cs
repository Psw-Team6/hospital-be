using System;
using System.Collections.Generic;
using HospitalLibrary.BloodConsumptions.Model;

namespace HospitalLibrary.BloodUnits.Model
{
    public class BloodUnit
    {
        private Guid _id;
        private BloodType _bloodType;
        private int _amount;
        private String _bloodBankName;
        private IEnumerable<BloodConsumption> _consumptions;

        public BloodUnit(Guid id, BloodType bloodType, int amount, string bloodBankName)
        {
            Id = id;
            BloodType = bloodType;
            Amount = amount;
            BloodBankName = bloodBankName;
            Consumptions = new List<BloodConsumption>();
        }

        public void decreseAmount(int consumptionAmount)
        {
            if(isValidToDecrese(consumptionAmount))
                Amount -=consumptionAmount;
        }

        private bool isValidToDecrese(int consumptionAmount)
        {
           return consumptionAmount <= Amount ? true : false;
        }
        
        public IEnumerable<BloodConsumption> Consumptions
        {
            get=> _consumptions;
            set=> _consumptions = value;
        }
        
        public int Amount
        {
            get=> _amount;
            set=> _amount = value;
        }
        
        public Guid Id
        {
            get=> _id;
            set=> _id = value;
        }
        
        public BloodType BloodType
        {
            get=> _bloodType;
            set=> _bloodType = value;
        }
        
        public string BloodBankName
        {
            get=> _bloodBankName;
            set=> _bloodBankName = value;
        }
        
        public BloodUnit(BloodType bloodType, int amount, string bloodBankName)
        {
        }
        
    }
}