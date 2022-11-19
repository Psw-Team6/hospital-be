using AutoMapper;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.PDFReport.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SendGrid;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFReportController
    {
        private readonly PDFReportService _pDFReportService;
        private readonly IMapper _mapper;
        static HttpClient httpClient = new HttpClient();
        public PDFReportController(PDFReportService pDFReportService, IMapper mapper)
        {
            _pDFReportService = pDFReportService;
            _mapper = mapper;
        }

      
        static async Task UploadPDF (string path, byte[] paramFileBytes)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StreamContent(new MemoryStream(paramFileBytes)), "file", "banka.pdf");
            
           // form.Add(new ByteArrayContent(paramFileBytes));
            HttpResponseMessage response = await httpClient.PostAsync(path, form);
            Console.WriteLine(form.ReadAsStringAsync());
           // response.EnsureSuccessStatusCode();
           //httpClient.Dispose();
        }

        // POST api/pdfreport
        [HttpPost]
        public async void sendReport(String bankName, int generatePeriod)
        {
           await UploadPDF("http://localhost:8080/api/PDFReport/" + bankName, _pDFReportService.CreateDocument());
        }

    }
}
