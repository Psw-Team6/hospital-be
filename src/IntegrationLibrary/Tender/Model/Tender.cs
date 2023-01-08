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

        public Tender() { }

        public Tender(Guid id, Boolean hasDeadline, DateTime deadlineDate, DateTime publishedDate, StatusTender status, TenderOffer winner
            , IEnumerable<BloodUnitAmount> bloodUnitAmount, IEnumerable<TenderOffer> tenderOffer)
        {
            Id = id;
            HasDeadline = hasDeadline;
            DeadlineDate = deadlineDate;
            PublishedDate = publishedDate;
            Status = status;
            Winner = winner;
            BloodUnitAmount = bloodUnitAmount;
            TenderOffer = tenderOffer;
            Validate();
        }

        private void Validate()
        {
            if (DeadlineDate.CompareTo(DateTime.Now) < 0)
                throw new Exception("DeadlineDate is in past!");
            else if (PublishedDate.CompareTo(DateTime.Now) < 0)
                throw new Exception("Published date is in past!");
        }

        public void addTenderOffer(TenderOffer tenderOffer)
        {
            if (TenderOffer == null)
                TenderOffer = new TenderOffer[] { tenderOffer };
            else
                if (TenderOffer.Where(d => d.BloodBankName == tenderOffer.BloodBankName) == null)
                TenderOffer = TenderOffer.Concat(new[] { tenderOffer });
            else
            {
                TenderOffer = TenderOffer.Where(d => d.BloodBankName != tenderOffer.BloodBankName);
                TenderOffer = TenderOffer.Concat(new[] { tenderOffer });
            }

        }

        public void CloseTender()
        {
            if (Status == StatusTender.InProcess)
            {
                Status = StatusTender.Close;
            }
        }

        public void OpenTender()
        {
            Status = StatusTender.Open;
        }

        public void SetTenderInProcess()
        {
            if (Status == StatusTender.Open)
            {
                Status = StatusTender.InProcess;
            }
            else
            {
                throw new Exception("Tender isn't open!");
            }
        }

        public bool HasTenderDeadLine()
        {
            return HasDeadline;
        }

        public void SetWinnerOfTender(TenderOffer winner)
        {
            if (Status == StatusTender.Open)
            {
                Winner = winner;
            }
            else
            {
                throw new Exception("Tender isn't open!");
            }
        }

        public void ExtendDeadlineDate(DateTime newDeadlineDate)
        {
            if (Status == StatusTender.Open)
            {
                if (DeadlineDate.CompareTo(newDeadlineDate) < 0)
                {
                    DeadlineDate = newDeadlineDate;
                }
                else
                {
                    throw new Exception("New date is before old date!");
                }
            }
            else
            {
                throw new Exception("Tender isn't open!");
            }
        }

        public void ShortenDeadlineDate(DateTime newDeadlineDate)
        {
            if (Status == StatusTender.Open)
            {
                if (DeadlineDate.CompareTo(newDeadlineDate) > 0)
                {
                    DeadlineDate = newDeadlineDate;
                }
                else
                {
                    throw new Exception("New date is after the old date!");
                }
            }
            else
            {
                throw new Exception("Tender isn't open!");
            }
        }

        public void AddBloodUnit(BloodUnitAmount bloodUnit) 
        {
            bool flag = false;
            foreach(BloodUnitAmount bu in BloodUnitAmount) 
            {
                if (bu.BloodType == bloodUnit.BloodType) 
                {
                    bloodUnit.Amount += bu.Amount;
                    flag = true;
                }
            }

            if (flag) 
            {
                BloodUnitAmount.Concat(new[] {bloodUnit});
            }
        }
    
    }
}
