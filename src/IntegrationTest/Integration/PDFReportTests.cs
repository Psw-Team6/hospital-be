using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.NewsFromBloodBank.Model;
using IntegrationLibrary.NewsFromBloodBank.Service;
using IntegrationLibrary.PDFReports.Service;
using IntegrationTest.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Integration
{
    public class PDFReportTests : BaseIntegrationTest
    {
        public PDFReportTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static PDFReportController SetupController(IServiceScope scope)
        {
            return new PDFReportController(scope.ServiceProvider.GetRequiredService<IPDFReportService>());
        }

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

        [Fact]
        public void SendPDFReport_Successfuly()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            String bankName = "Moja Banka Krvi";
            int genPer = 60;
            var result = controller.sendReport("Test "+bankName, genPer);
   

            result.ShouldBeOfType<OkResult>();
        }

    }
}
