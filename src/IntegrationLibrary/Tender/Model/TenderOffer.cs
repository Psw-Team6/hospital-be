using IntegrationLibrary.BloodBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.Model
{
    public class TenderOffer
    {
        public string BloodBankName { get; set; }
        public DateTime RealizationDate { get; set; }
        public double Price { get; set; }

        public Boolean isThePricePositive()
        {
            if (Price>0)
                return true;
            else return false;
                
        }

        public Boolean isBloodBankNameNotEmpty()
        {
            if (BloodBankName.Equals(""))
                return false;
            else return true;
        }

        public Boolean isRealizationDateInFuture()
        {
            if (RealizationDate > DateTime.Now)
                return true;
            else
                return false;
        }
    }    
}
