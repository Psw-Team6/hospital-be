using AutoMapper;
using AutoMapper.Internal.Mappers;
using IntegrationAPI.Dtos.Response;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.PDFReports.Model;
using IntegrationLibrary.PDFReports.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFReportController
    {
        private readonly PDFReportService pDFReportService;
        private readonly IMapper mapper;
        static HttpClient httpClient = new HttpClient();
        public PDFReportController(PDFReportService pDFReportService, IMapper mapper)
        {
            this.pDFReportService = pDFReportService;
            this.mapper = mapper;
        }


        static async Task UploadPDF(string path, byte[] paramFileBytes, string bankName)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StreamContent(new MemoryStream(paramFileBytes)), "file", bankName + ".pdf");
            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(path, form);
            } catch (HttpRequestException httpEx)
            {
            }
            
           

        }

        // POST api/pdfreport
        [HttpPost]
        public async void sendReport(String bankName, int generatePeriod)
        {

           var consumptions = await httpClient.GetFromJsonAsync<List<BloodConsumptionPDFReport>>("http://localhost:5000/api/v1/BloodConsumption/getBankConsumptions/" + bankName);
           
            await UploadPDF("http://localhost:8080/api/PDFReport/" + bankName, pDFReportService.CreateDocument(new PDFReport(generatePeriod, bankName, consumptions)), bankName);
        }


    }
}
