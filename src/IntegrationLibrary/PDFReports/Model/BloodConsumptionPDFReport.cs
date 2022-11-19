using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReports.Model
{
    public class BloodConsumptionPDFReport
    {
        public BloodType bloodType { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
