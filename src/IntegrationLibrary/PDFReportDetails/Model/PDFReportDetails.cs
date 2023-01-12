using IntegrationLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReportDetails.Model
{
    public class PDFReportDetails
    {
        public Guid Id { get; set; }
        public String PdfName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public PDFReportType Type { get; set; }
    }
}
