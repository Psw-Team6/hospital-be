using IntegrationLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.Model
{
    public class Tender
    {
        public Guid Id { get; set; }
        public Boolean HasDeadline { get; set; }
        public DateTime DeadlineDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public StatusTender Status { get; set; }
        public TenderOffer Winner { get; set; }
        public IEnumerable<BloodUnitAmount> BloodUnitAmount { get; set; }
        public IEnumerable<TenderOffer> TenderOffer { get; set; }


        public void addTenderOffer(TenderOffer tenderOffer)
        {
            if (TenderOffer==null)
                TenderOffer=new TenderOffer[] { tenderOffer };
            else
                if (TenderOffer.Where(d=> d.BloodBankName==tenderOffer.BloodBankName)==null)
                    TenderOffer =TenderOffer.Concat(new[] { tenderOffer});
                else
                    {
                    TenderOffer = TenderOffer.Where(d => d.BloodBankName != tenderOffer.BloodBankName);
                    TenderOffer = TenderOffer.Concat(new[] { tenderOffer });
                     }
                    
        }
    }
}
