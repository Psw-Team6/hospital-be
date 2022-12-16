using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodRequests.Model
{
    public class BloodRequest
    {
        public Guid Id { get; set; }
        public BloodType Type { get; set; }
        public double Amount { get; set; }
        public String Reason { get; set; }
        public DateTime Date { get; set; }
        public String DoctorUsername { get; set; } 
        public Status Status { get; set; }
        public String Comment { get; set; }

        public Guid BloodBankId { get; set; }

        
        public bool isPending()
        {
            if (this.Status == Status.PENDING)
                return true;
            else
                return false;
        }

        public bool isApproved()
        {
            if (this.Status == Status.APPPROVED)
                return true;
            else
                return false;

        }

        public bool isRejected()
        {
            if (this.Status == Status.REJECTED)
                return true;
            else
                return false;

        }

    }

}
