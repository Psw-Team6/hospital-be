using AutoMapper;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodConsumptions.Service;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.PDFReports.Model;
using IntegrationLibrary.PDFReports.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BloodConsumption = HospitalLibrary.BloodConsumptions.Model.BloodConsumption;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFReportController
    {
        private readonly PDFReportService pDFReportService;
        private readonly BloodConsumptionService bloodConsumptionService;
        private readonly IMapper mapper;
        static HttpClient httpClient = new HttpClient();
        public PDFReportController(PDFReportService pDFReportService, BloodConsumptionService bloodConsumptionService, IMapper mapper)
        {
            this.bloodConsumptionService = bloodConsumptionService;
            this.pDFReportService = pDFReportService;
            this.mapper = mapper;
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
            Task<IEnumerable <BloodConsumption>> bloodConsumptions = bloodConsumptionService.GetByBloodBankName(bankName);           
            var result = mapper.Map<List<BloodConsumptionPDFReport>>(new List<BloodConsumption>(bloodConsumptions.Result.ToList()));
            await UploadPDF("http://localhost:8080/api/PDFReport/" + bankName, pDFReportService.CreateDocument(new PDFReport(bankName, generatePeriod, result)));
        }

    }
}
