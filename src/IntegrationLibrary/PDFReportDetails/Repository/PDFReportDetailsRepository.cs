using IntegrationLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.PDFReportDetails.Repository
{
    public class PDFReportDetailsRepository : IPDFReportDetailsRepository
    {
        private readonly IntegrationDbContext _context;
        public PDFReportDetailsRepository(IntegrationDbContext context)
        {
            _context = context;
        }
        public void Create(Model.PDFReportDetails details)
        {
            _context.PDFReportDetails.Add(details);
            _context.SaveChanges();
        }

        public IEnumerable<Model.PDFReportDetails> GetAll()
        {
            return _context.PDFReportDetails.ToList();
        }
    }
}
