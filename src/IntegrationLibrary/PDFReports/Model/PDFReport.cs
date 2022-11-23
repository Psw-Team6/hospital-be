using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReports.Model
{
    public class PDFReport
    {
        public int generatePeriod { get; set; }
        public string bankName { get; set; }
        public List<BloodConsumptionPDFReport> bloodConsumptions { get; set; }

        public PDFReport(int generatePeriod, string bankName, List<BloodConsumptionPDFReport> bloodConsumptions)
        {
            this.generatePeriod = generatePeriod;
            this.bankName = bankName;
            this.bloodConsumptions = bloodConsumptions;
        }

        public List<BloodConsumptionPDFReport> GetBloodConsumptionsForPeriod(DateTime startDate, DateTime endDate, BloodType bloodType)
        {
            List <BloodConsumptionPDFReport> bloodConsumptionPDFReports = new List <BloodConsumptionPDFReport>();
            foreach (BloodConsumptionPDFReport report in bloodConsumptions)
            {
                if (report.Date>=startDate && report.Date<=endDate && report.BloodUnit.BloodType== bloodType)
                {
                    bloodConsumptionPDFReports.Add(report);
                }
            }
            return bloodConsumptionPDFReports;
        }

        public int GetBloodConsumptionsAmountForPeriod(DateTime startDate, DateTime endDate, BloodType bloodType)
        {
            int amount =0;
            foreach (BloodConsumptionPDFReport report in bloodConsumptions)
            {
                if (report.Date >= startDate && report.Date <= endDate && report.BloodUnit.BloodType == bloodType)
                {
                    amount= amount + report.Amount;
                }
            }
            return amount;
        }
    }
}
