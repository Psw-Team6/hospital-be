using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReports.Model
{
    public class BloodUnit
    {
        public Guid Id { get; set; }
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public String BloodBankName { get; set; }
        public IEnumerable<BloodConsumptionPDFReport> Consumptions { get; set; }

    }
}
