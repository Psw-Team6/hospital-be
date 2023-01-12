using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodStatistic.Model
{
    public class BloodStatisticResponse
    {
        public Guid BloodBankID { get; set; }
        public int Aneg { get; set; }
        public int Apos { get; set; }
        public int Bneg { get; set; }
        public int Bpos { get; set; }
        public int ABpos { get; set; }
        public int ABneg { get; set; }
        public int Opos { get; set; }
        public int Oneg { get; set; }
        public DateRange DateRange { get; set; }
    }
}
