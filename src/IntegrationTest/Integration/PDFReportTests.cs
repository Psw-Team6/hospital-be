using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.NewsFromBloodBank.Service;
using IntegrationLibrary.PDFReports.Service;
using IntegrationTest.Setup;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Integration
{
    public class PDFReportTests
    {


        [Fact]
        public void Send_PDF_Report_to_Blood_Bank()
        {
            
            String bankName = "Moja Banka Krvi";
            String path = "http://localhost:8080/api/PDFReport/" + bankName;
            int genPer = 60;
            var mockNotify = new Mock<IPDFReportService>();
            PDFReportController pDFReportController = new PDFReportController(mockNotify.Object);
            pDFReportController.sendReport(bankName, genPer);
            mockNotify.Verify(x => x.UploadPDF(path, bankName, genPer), Times.Once);
        }

    }
}
