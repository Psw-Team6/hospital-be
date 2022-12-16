using IntegrationLibrary.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodSubscription.Model
{
    public class AmountOfBloodType
    {
        public BloodType bloodType { get; private set; }
        public int amount { get; private set;}

        public AmountOfBloodType(BloodType bloodType, int amount)
        {
            this.bloodType = bloodType;
            this.amount = amount;
            Validate();
        }
        private void Validate() 
        {
            if (this.amount < 0) 
                throw new ArgumentOutOfRangeException(nameof(amount));
        }

        private bool isSameType(AmountOfBloodType aobt)
        {
            if (this.bloodType != aobt.bloodType)
                return false;
            return true;
        }

        public AmountOfBloodType Add(AmountOfBloodType aobt) 
        {
            if (!isSameType(aobt))
                throw new Exception();

            return new AmountOfBloodType(aobt.bloodType, aobt.amount + this.amount);
        }
    }
}
