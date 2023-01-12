using IntegrationLibrary.BloodStatistic.Model;
using IntegrationLibrary.PDFReports.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReports.Service
{
    public interface IPDFReportService
    {
        public byte[] CreateDocument(PDFReport report);
        public bool UploadPDF(string path, String bankName, int generatePeriod);
        public List<BloodConsumptionPDFReport> GetConsumptions(string bankName);
        public byte[] CreateDocumentInRange(List<BloodStatisticResponse> bloodStatistics);
    }
}
