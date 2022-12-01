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

    }
}
