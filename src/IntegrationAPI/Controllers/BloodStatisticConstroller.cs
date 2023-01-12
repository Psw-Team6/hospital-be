
using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using IntegrationLibrary.HTTP;
using IntegrationLibrary.BloodRequests.Model;
using IntegrationAPI.Dtos.Response;
using IntegrationLibrary.BloodStatistic.Model;
using IntegrationLibrary.BloodStatistic.Service;
using Newtonsoft.Json;
using System.Net.Http.Json;
using IntegrationLibrary.PDFReportDetails.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using IntegrationLibrary.PDFReportDetails.Service;
using IntegrationLibrary.SFTP.Service;
using IntegrationLibrary.PDFReports.Service;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodStatisticController : ControllerBase
    {
        private readonly IBloodStatisticService bloodStatisticService;
        private static readonly HttpClient client = new HttpClient();
        private readonly IPDFReportDetailsService pDFReportDetailsService;
        private readonly ISFTPService sFTPService;
        private readonly IPDFReportService pDFReportService;

        public BloodStatisticController(IBloodStatisticService bloodStatisticService, IPDFReportDetailsService pDFReportDetailsService, ISFTPService sFTPService, IPDFReportService pDFReportService)
        {
            this.bloodStatisticService = bloodStatisticService;
            this.pDFReportDetailsService = pDFReportDetailsService;
            this.sFTPService = sFTPService;
            this.pDFReportService = pDFReportService;
        }

        [HttpPut("getTenderStatistic")]
        public List<BloodStatisticResponse> getTenderStatistic(DateRange range)
        {
            String pdfName = "TenderStatistic " + range.From.Day.ToString() + "-" + range.From.Month.ToString() + "-" + range.From.Year.ToString() + "  " + range.To.Day.ToString() + "-" + range.To.Month.ToString() + "-" + range.To.Year.ToString() + ".pdf";
            //String pdfName = "proba.pdf";
            PDFReportDetails details = new PDFReportDetails(pdfName, range.From, range.To, IntegrationLibrary.Enums.PDFReportType.Tender);


                sFTPService.UploadFileToRebexServer(pDFReportService.CreateDocumentInRange(bloodStatisticService.getTenderStatistic(range)), pdfName); 
                pDFReportDetailsService.Create(details);

            
                    
            return bloodStatisticService.getTenderStatistic(range);
        }

        [HttpPut("getUrgentStatistic")]
        public List<BloodStatisticResponse> getUrgentStatistic(DateRange range)
        {
            List<BloodUnit> units = getUrgentUnits().Result.Value;
            return bloodStatisticService.getUrgentStatistic(range, units);
        }


        [HttpGet("getUrgentUnits")]
        public async Task<ActionResult<List<BloodUnit>>> getUrgentUnits()
        {
            try
            {
                var result = client.GetFromJsonAsync<List<BloodUnit>>("http://localhost:5000/api/v1/BloodUnit/getUrgentUnits").Result;
                return result;
            }
            catch (HttpRequestException httpEx)
            {
                if (httpEx.StatusCode.HasValue)
                {
                    var p = (int)httpEx.StatusCode;
                }
                else
                {
                    return BadRequest();
                }

            }

            return null;
        }
    }
}
