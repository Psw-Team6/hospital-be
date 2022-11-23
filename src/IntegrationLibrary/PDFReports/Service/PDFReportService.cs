using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using IntegrationLibrary.PDFReports.Model;

namespace IntegrationLibrary.PDFReports.Service
{
    public class PDFReportService
    {
        
        public byte[] CreateDocument(PDFReport report)
        {
            PdfDocument document = new PdfDocument();

            //Set the page size.
            document.PageSettings.Size = PdfPageSize.A4;

            //Add a page to the document.
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;

            //Set the font.
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-report.generatePeriod);

            //Draw the text.
            graphics.DrawString("Report", new PdfStandardFont(PdfFontFamily.Helvetica, 20), PdfBrushes.Black, new Syncfusion.Drawing.PointF(150,0));
            graphics.DrawString(report.bankName, new PdfStandardFont(PdfFontFamily.Helvetica, 20), PdfBrushes.Black, new Syncfusion.Drawing.PointF(150, 50));          
            graphics.DrawString("Report from: " + startDate.ToShortDateString() + " to: " + endDate.ToShortDateString(), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 150));
            graphics.DrawString("Consumption of blood units:", new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 200));
            graphics.DrawString("A-: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Aneg), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 250));
            graphics.DrawString("A+: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Apos), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 300));
            graphics.DrawString("B-: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Bneg), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 350));
            graphics.DrawString("B+: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Bpos), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 400));
            graphics.DrawString("AB-: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.ABneg), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 450));
            graphics.DrawString("AB+: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.ABpos), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 500));
            graphics.DrawString("O-: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Oneg), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 550));
            graphics.DrawString("O+: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Opos), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 600));
            //Create file stream.
           // using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Output.pdf"), FileMode.Create, FileAccess.ReadWrite))
          //  {
                //Save the PDF document to file stream.
          //      document.Save(outputFileStream);
        //    }
  
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Close the document.
            document.Close(true);
            byte[] docBytes = stream.ToArray();
          
            return docBytes;
        }

       
    }
}
