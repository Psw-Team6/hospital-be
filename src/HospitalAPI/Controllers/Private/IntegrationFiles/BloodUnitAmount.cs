using IntegrationLibrary.BloodRequests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.Model
{
    public class BloodUnitAmount
    {
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }

        public Guid TenderId { get; set; }

        public Tender Tender { get; set; }

        public Guid Id { get; set; }

        public BloodUnitAmount() { }

        public BloodUnitAmount(BloodType bloodType, int amount, Tender tender, Guid id) 
        {
            BloodType = bloodType;
            Amount = amount;    
            TenderId = tender.Id;
            Tender = tender;
            Id = id;
            Validate();
        }

        public BloodUnitAmount(BloodType bloodType, int amount, Tender tender)
        {
            BloodType = bloodType;
            Amount = amount;
            TenderId = tender.Id;
            Tender = tender;
            Validate();
        }

        private void Validate() 
        {
            if (Amount <= 0) 
            {
                throw new Exception("Amount can not be less than 0!");
            }
        }

        public bool IsSameBloodType(BloodUnitAmount bu) 
        {
            if (BloodType == bu.BloodType)
                return true;

            return false;
        }

        public void Add(int amount) 
        {
            Amount += amount;
        }
    }
}
