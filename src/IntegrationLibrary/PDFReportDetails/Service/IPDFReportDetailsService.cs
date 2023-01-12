using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReportDetails.Service
{
    public interface IPDFReportDetailsService
    {
        void Create(PDFReportDetails.Model.PDFReportDetails details);
        IEnumerable<PDFReportDetails.Model.PDFReportDetails> GetAll();
    }
}
