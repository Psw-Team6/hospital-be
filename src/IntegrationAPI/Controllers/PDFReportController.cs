using AutoMapper;
using AutoMapper.Internal.Mappers;
using IntegrationAPI.Dtos.Response;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.BloodRequests.Model;
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
    public class PDFReportController: ControllerBase
    {
        private readonly IPDFReportService pDFReportService;
  
        static HttpClient httpClient = new HttpClient();
        public PDFReportController(IPDFReportService pDFReportService)
        {
            this.pDFReportService = pDFReportService;
   
        }

       /* [HttpPost("uploadPDF")]
        public  ActionResult UploadPDF(string path, byte[] paramFileBytes, string bankName)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StreamContent(new MemoryStream(paramFileBytes)), "file", bankName + ".pdf");
            try
            {
                HttpResponseMessage response =  httpClient.PostAsync(path, form).Result;
            } catch (HttpRequestException httpEx)
            {
                return BadRequest();
            }

            return Ok();

        }*/

        // POST api/pdfreport
        [HttpPost]
        public  ActionResult sendReport(String bankName, int generatePeriod)
        {
          /*  List<BloodConsumptionPDFReport> consumptions;
            try
            {
                consumptions =  httpClient.GetFromJsonAsync<List<BloodConsumptionPDFReport>>("http://localhost:5000/api/v1/BloodConsumption/getBankConsumptions/" + bankName).Result;
            } catch
            {
                return BadRequest();
            }*/

          try
            {
                pDFReportService.UploadPDF("http://localhost:8080/api/PDFReport/" + bankName,  bankName, generatePeriod);
            } catch
            {
                return BadRequest();
            }

            return Ok();

        }


    }
}
