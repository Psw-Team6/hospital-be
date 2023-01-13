using IntegrationLibrary.PDFReportDetails.Model;
using IntegrationLibrary.PDFReportDetails.Service;
using IntegrationLibrary.SFTP.Service;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFReportDetailsController : Controller
    {
        
        private readonly IPDFReportDetailsService _detaisService;
        private readonly ISFTPService _sftpService;

        public PDFReportDetailsController(IPDFReportDetailsService detailsService, ISFTPService sftpService)
        {
            _detaisService = detailsService;
            _sftpService = sftpService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_detaisService.GetAll());
        }

        [HttpPost]
        public ActionResult Create(PDFReportDetails details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _detaisService.Create(details);
            return CreatedAtAction("Create", new { id = details.Id }, details);
        }

        [HttpGet("{pdfName}")]
        public ActionResult GetPDFReport(string pdfName)
        {
            return File(_sftpService.DownloadFileFromRebexServer(pdfName), "application/pdf", pdfName);
        }
    }
}
