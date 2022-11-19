using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReports.Model
{
    public class PDFReport
    {
        public string bankName { get; set; }
        public int generatePeriod { get; set; }
        public List<BloodConsumptionPDFReport> bloodConsumptions { get; set; }

        public PDFReport(string bankName, int generatePeriod, List<BloodConsumptionPDFReport> bloodConsumptions)
        {
            this.bankName = bankName;
            this.generatePeriod = generatePeriod;
            this.bloodConsumptions = bloodConsumptions;
        }
    }
}
