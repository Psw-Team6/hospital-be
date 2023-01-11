using System;
using System.Collections.Generic;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.IO;
using IntegrationLibrary.PDFReports.Model;
using System.Net.Http;
using System.Net.Http.Json;
using IntegrationLibrary.SendMail.Services;
using IntegrationLibrary.BloodBank.Repository;
using Syncfusion.Pdf.Tables;
using System.Drawing;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.PageElements.Charting.Axes;
using ceTe.DynamicPDF.PageElements.Charting.Series;
using ceTe.DynamicPDF.PageElements.Charting;

namespace IntegrationLibrary.PDFReports.Service
{
    public class PDFReportService : IPDFReportService
    {


        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IEmailService _emailService;

        public PDFReportService(IBloodBankRepository bloodBankRepository, IEmailService emailService)
        {
            _bloodBankRepository = bloodBankRepository;
            _emailService = emailService;
        }

        public byte[] CreateDocument(PDFReport report)
        {
            Document document = new Document();

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-report.generatePeriod);

            string labelText = "Report\n\n\nReport from:" + startDate.ToShortDateString() + " to:" + endDate.ToShortDateString();
            Label label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, ceTe.DynamicPDF.TextAlign.Center);
            page.Elements.Add(label);

            if (report.bloodConsumptions == null)
            {
                labelText = "No Consumption";
                label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, ceTe.DynamicPDF.TextAlign.Center);
                page.Elements.Add(label);
            }
            else
            {
               
                Table2 table = new Table2(0, 150, 300, 300);
                table.CellDefault.Border.Color = RgbColor.Blue;
                table.CellSpacing = 5.0f;

                // Add columns to the table
                table.Columns.Add(150);
                table.Columns.Add(120);


                // Add rows to the table and add cells to the rows
                Row2 row = table.Rows.Add(30, Font.HelveticaBold, 16, RgbColor.Black,
                RgbColor.Gray);
                row.CellDefault.Align = ceTe.DynamicPDF.TextAlign.Center;
                row.CellDefault.VAlign = VAlign.Center;
                row.Cells.Add("Blood Type");
                row.Cells.Add("Consumption, units");


                Row2 row1 = table.Rows.Add(20);
                Cell2 cell = row1.Cells.Add("A positive", Font.HelveticaBold, 16,
                    RgbColor.Black, RgbColor.Gray, 1);
                cell.Align = ceTe.DynamicPDF.TextAlign.Center;
                cell.VAlign = VAlign.Center;
                row1.Cells.Add(report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Apos).ToString());

                Row2 row2 = table.Rows.Add(20);
                Cell2 cell1 = row2.Cells.Add("A negative", Font.HelveticaBold, 16,
                    RgbColor.Black, RgbColor.Gray, 1);
                cell1.Align = ceTe.DynamicPDF.TextAlign.Center;
                cell1.VAlign = VAlign.Center;
                row2.Cells.Add(report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Aneg).ToString());

                Row2 row3 = table.Rows.Add(20);
                Cell2 cell2 = row3.Cells.Add("B positive", Font.HelveticaBold, 16,
                    RgbColor.Black, RgbColor.Gray, 1);
                cell2.Align = ceTe.DynamicPDF.TextAlign.Center;
                cell2.VAlign = VAlign.Center;
                row3.Cells.Add(report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Bpos).ToString());

                Row2 row4 = table.Rows.Add(20);
                Cell2 cell3 = row4.Cells.Add("B negative", Font.HelveticaBold, 16,
                    RgbColor.Black, RgbColor.Gray, 1);
                cell3.Align = ceTe.DynamicPDF.TextAlign.Center;
                cell3.VAlign = VAlign.Center;  
                row4.Cells.Add(report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Bneg).ToString());

                Row2 row5 = table.Rows.Add(20);
                Cell2 cell4 = row5.Cells.Add("AB positive", Font.HelveticaBold, 16,
                    RgbColor.Black, RgbColor.Gray, 1);
                cell4.Align = ceTe.DynamicPDF.TextAlign.Center;
                cell4.VAlign = VAlign.Center;
                row5.Cells.Add(report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.ABpos).ToString());

                Row2 row6 = table.Rows.Add(20);
                Cell2 cell5 = row6.Cells.Add("AB negative", Font.HelveticaBold, 16,
                    RgbColor.Black, RgbColor.Gray, 1);
                cell5.Align = ceTe.DynamicPDF.TextAlign.Center;
                cell5.VAlign = VAlign.Center;
                row6.Cells.Add(report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.ABneg).ToString());

                Row2 row7 = table.Rows.Add(20);
                Cell2 cell6 = row7.Cells.Add("O positive", Font.HelveticaBold, 16,
                    RgbColor.Black, RgbColor.Gray, 1);
                cell6.Align = ceTe.DynamicPDF.TextAlign.Center;
                cell6.VAlign = VAlign.Center;
                row7.Cells.Add(report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Opos).ToString());

                Row2 row8 = table.Rows.Add(20);
                Cell2 cell7 = row8.Cells.Add("O negative", Font.HelveticaBold, 16,
                    RgbColor.Black, RgbColor.Gray, 1);
                cell7.Align = ceTe.DynamicPDF.TextAlign.Center;
                cell7.VAlign = VAlign.Center;
                row8.Cells.Add(report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Oneg).ToString());

                // Add the table to the page
                page.Elements.Add(table);

                Chart chart = new Chart(0, 400, 400, 230);
                PlotArea plotArea = chart.PrimaryPlotArea;

                Title title1 = new Title("Blood Consumption");
                chart.HeaderTitles.Add(title1);

                IndexedColumnSeries columnSeries1 = new IndexedColumnSeries("Website A");
                columnSeries1.Values.Add(new float[] { 5, 7, 9, 6 });
                IndexedColumnSeries columnSeries2 = new IndexedColumnSeries("Website B");
                columnSeries2.Values.Add(new float[] { 4, 2, 5, 8 });
                IndexedColumnSeries columnSeries3 = new IndexedColumnSeries("Website C");
                columnSeries3.Values.Add(new float[] { 2, 4, 6, 9 });

                AutoGradient autogradient1 = new AutoGradient(180f, CmykColor.Red, CmykColor.IndianRed);
                columnSeries1.Color = autogradient1;
                AutoGradient autogradient2 = new AutoGradient(180f, CmykColor.Green, CmykColor.YellowGreen);
                columnSeries2.Color = autogradient2;
                AutoGradient autogradient3 = new AutoGradient(180f, CmykColor.Blue, CmykColor.LightBlue);
                columnSeries3.Color = autogradient3;

                plotArea.Series.Add(columnSeries1);
                plotArea.Series.Add(columnSeries2);
                plotArea.Series.Add(columnSeries3);

                Title lTitle = new Title("Visitors (in millions)");
                columnSeries1.YAxis.Titles.Add(lTitle);

                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("Q1", 0));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("Q2", 1));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("Q3", 2));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("Q4", 3));

                page.Elements.Add(chart);
            }




            MemoryStream stream = new MemoryStream();
            document.Draw(stream);
            //Close the document.
            byte[] docBytes = stream.ToArray();

            return docBytes;

        }

        public byte[] CreateDocument1(PDFReport report)
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
            graphics.DrawString("Report", new PdfStandardFont(PdfFontFamily.Helvetica, 20), PdfBrushes.Black, new Syncfusion.Drawing.PointF(150, 0));
            graphics.DrawString(report.bankName, new PdfStandardFont(PdfFontFamily.Helvetica, 20), PdfBrushes.Black, new Syncfusion.Drawing.PointF(150, 50));
            graphics.DrawString("Report from: " + startDate.ToShortDateString() + " to: " + endDate.ToShortDateString(), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 150));
            if (report.bloodConsumptions == null)
            {
                graphics.DrawString("No consumption.", new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 200));
            }
            else
            {
                //Declare a PdfLightTable
                PdfLightTable pdfLightTable = new PdfLightTable();

                //Set the Data source as direct
                pdfLightTable.DataSourceType = PdfLightTableDataSourceType.TableDirect;
                PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                // Alternative cell style
                PdfCellStyle altStyle = new PdfCellStyle(font, PdfBrushes.White, PdfPens.Green);
                altStyle.BackgroundBrush = PdfBrushes.DarkGray;
                // Table header cell style
                PdfCellStyle headerStyle = new PdfCellStyle(font, PdfBrushes.White, PdfPens.Brown);
                headerStyle.BackgroundBrush = PdfBrushes.Red;
                //Set the table style
                pdfLightTable.Style.AlternateStyle = altStyle;
                pdfLightTable.Style.HeaderStyle = headerStyle;

                //Create columns
                pdfLightTable.Columns.Add(new PdfColumn("Blood Unit"));
                pdfLightTable.Columns.Add(new PdfColumn("Consumption, units"));

                //Add rows
                pdfLightTable.Rows.Add(new object[] { "Blood Unit", "Consumption, units" });
                pdfLightTable.Rows.Add(new object[] { "A positive", report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Apos).ToString() });
                pdfLightTable.Rows.Add(new object[] { "A negative", report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Aneg).ToString() });
                pdfLightTable.Rows.Add(new object[] { "B positive", report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Bpos).ToString() });
                pdfLightTable.Rows.Add(new object[] { "B negative", report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Bneg).ToString() });
                pdfLightTable.Rows.Add(new object[] { "AB positive", report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.ABpos).ToString() });
                pdfLightTable.Rows.Add(new object[] { "AB negative", report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.ABneg).ToString() });
                pdfLightTable.Rows.Add(new object[] { "O positive", report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Opos).ToString() });
                pdfLightTable.Rows.Add(new object[] { "O negative", report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Oneg).ToString() });

                //Draw the PdfLightTable
                pdfLightTable.Draw(page, new Syncfusion.Drawing.PointF(0, 250));

                //graphics.DrawString("Consumption of blood units:", new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 200));
                //graphics.DrawString("A-: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Aneg), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 250));
                //graphics.DrawString("A+: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Apos), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 300));
                //graphics.DrawString("B-: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Bneg), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 350));
                //graphics.DrawString("B+: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Bpos), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 400));
                //graphics.DrawString("AB-: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.ABneg), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 450));
                //graphics.DrawString("AB+: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.ABpos), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 500));
                //graphics.DrawString("O-: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Oneg), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 550));
                //graphics.DrawString("O+: " + report.GetBloodConsumptionsAmountForPeriod(startDate, endDate, BloodType.Opos), new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 600));

              
               

            }
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

        static HttpClient httpClient = new HttpClient();
        public bool UploadPDF(string path, String bankName, int generatePeriod)
        {
            PDFReport report = new PDFReport(generatePeriod, bankName, GetConsumptions(bankName));
            MultipartFormDataContent form = new MultipartFormDataContent();
            byte[] paramFileBytes = CreateDocument(report);

            BloodBank.BloodBank bloodBank = _bloodBankRepository.GetByName(bankName);
            string text = "Dear Sir or Madam, in the attachment, we are sending you a report for the blood bank <strong>"+bankName+"</strong> for the last "+generatePeriod+" days.";
            
            if (bloodBank != null)
            {
                SendMail.Email email = new SendMail.Email(bloodBank.Email, "PSW-hospital", text, "");
                _emailService.SendEmailWithAttacment(email, paramFileBytes, bankName);
            }
            else
            {
                SendMail.Email emailDefaul = new SendMail.Email("psw.isa.mail@gmail.com", "PSW-hospital","", text);
                _emailService.SendEmailWithAttacment(emailDefaul, paramFileBytes, bankName);
            }
            

            form.Add(new StreamContent(new MemoryStream(paramFileBytes)), "file", report.bankName + ".pdf");
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(path, form).Result;
            }
            catch (HttpRequestException httpEx)
            {
                return false;
            }

            return true;

        }

        public List<BloodConsumptionPDFReport> GetConsumptions (string bankName)
        {
            List<BloodConsumptionPDFReport> consumptions;
            try
            {
                consumptions = httpClient.GetFromJsonAsync<List<BloodConsumptionPDFReport>>("http://localhost:5000/api/v1/BloodConsumption/getBankConsumptions/" + bankName).Result;
            }
            catch
            {
                return null;
            }
            
            return consumptions;
        }


    }
}
