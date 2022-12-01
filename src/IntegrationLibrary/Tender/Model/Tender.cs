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
        public StatusTender Status { get; set; }
        public IEnumerable<BloodUnitAmount> BloodUnitAmount { get; set; }

    }
}
