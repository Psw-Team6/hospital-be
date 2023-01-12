using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReportDetails.Repository
{
    public interface IPDFReportDetailsRepository
    {
        void Create(PDFReportDetails.Model.PDFReportDetails details);
        IEnumerable<PDFReportDetails.Model.PDFReportDetails> GetAll();
    }
}
