using IntegrationLibrary.Tender.Model;
using System;

namespace IntegrationAPI.Dtos.Request
{
    public class TenderOfferRequest
    {
        public Tender Tender { get; set; }
        public double Price { get; set; }
        public DateTime RealizationDate { get; set; }
        public string bloodBankName { get; set; }

        public TenderOffer convertTenderOffer()
        {
            TenderOffer tenderOffer = new TenderOffer();
            tenderOffer.Price = this.Price;
            tenderOffer.RealizationDate=this.RealizationDate;
            tenderOffer.BloodBankName=this.bloodBankName;
            return tenderOffer;

        }
    }
}
