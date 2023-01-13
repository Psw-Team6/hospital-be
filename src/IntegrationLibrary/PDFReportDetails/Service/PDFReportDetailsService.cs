using IntegrationLibrary.PDFReportDetails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReportDetails.Service
{
    public class PDFReportDetailsService : IPDFReportDetailsService
    {
        private readonly IPDFReportDetailsRepository _detailsRepository;

        public PDFReportDetailsService(IPDFReportDetailsRepository detailsRepository)
        {
            _detailsRepository = detailsRepository;
        }
        public void Create(Model.PDFReportDetails details)
        {
            _detailsRepository.Create(details);
        }

        public IEnumerable<Model.PDFReportDetails> GetAll()
        {
            return _detailsRepository.GetAll();
        }
    }
}
