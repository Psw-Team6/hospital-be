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
using IntegrationLibrary.BloodStatistic.Model;
using System.Linq;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.BloodStatistic.Service;

namespace IntegrationLibrary.PDFReports.Service
{
    public class PDFReportService : IPDFReportService
    {


        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IEmailService _emailService;
        private readonly IBloodBankService _bloodBankService;
        private readonly IBloodStatisticService _bloodStatisticService;

        public PDFReportService(IBloodBankRepository bloodBankRepository, IEmailService emailService, IBloodBankService bloodBankService, IBloodStatisticService bloodStatisticService)
        {
            _bloodBankRepository = bloodBankRepository;
            _emailService = emailService;
            _bloodBankService = bloodBankService;
            _bloodStatisticService = bloodStatisticService;
        }

        public byte[] CreateDocumentInRange(List<BloodStatisticResponse> bloodStatistics)
        {
            Document document = new Document();

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);



            if (bloodStatistics.Count == 0)
            {
                String labelText = "No Consumption";
                Label label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, ceTe.DynamicPDF.TextAlign.Center);
                page.Elements.Add(label);
            }
            else
            {
                DateTime endDate = bloodStatistics.First().DateRange.To;
                DateTime startDate = bloodStatistics.First().DateRange.From;

                string labelText = "Report\n\n\nReport from:" + startDate.ToShortDateString() + " to:" + endDate.ToShortDateString();
                Label label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, ceTe.DynamicPDF.TextAlign.Center);
                page.Elements.Add(label);

                for (int i = 0; i < bloodStatistics.Count; i++)
                {
                    labelText = _bloodBankService.GetById(bloodStatistics[i].BloodBankID).Name;
                    Table2 table = new Table2(0, 150, 300, 300);
                    int y = 135;
                    if (i > 0)
                    {
                        if (i % 2 == 0)
                        {
                            page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
                            document.Pages.Add(page);
                            y = 0;
                            table = new Table2(0, 20, 300, 300);
                        }
                        else
                        {
                            y = 380;
                            table = new Table2(0, 400, 300, 300);
                        }
                    }
                    label = new Label(labelText, 0, y, 504, 100, Font.Helvetica, 12, ceTe.DynamicPDF.TextAlign.Left);
                    page.Elements.Add(label);

                    table.CellDefault.Border.Color = RgbColor.Blue;
                    table.CellSpacing = 4.0f;

                    // Add columns to the table
                    table.Columns.Add(150);
                    table.Columns.Add(120);


                    Row2 row = table.Rows.Add(20, Font.HelveticaBold, 12, RgbColor.Black,
                    RgbColor.Gray);
                    row.CellDefault.Align = ceTe.DynamicPDF.TextAlign.Center;
                    row.CellDefault.VAlign = VAlign.Center;
                    row.Cells.Add("Blood Type");
                    row.Cells.Add("Amount, units");


                    Row2 row1 = table.Rows.Add(20);
                    Cell2 cell = row1.Cells.Add("A positive", Font.HelveticaBold, 12,
                        RgbColor.Black, RgbColor.Gray, 1);
                    cell.Align = ceTe.DynamicPDF.TextAlign.Center;
                    cell.VAlign = VAlign.Center;
                    row1.Cells.Add(bloodStatistics[i].Apos.ToString());

                    Row2 row2 = table.Rows.Add(20);
                    Cell2 cell1 = row2.Cells.Add("A negative", Font.HelveticaBold, 12,
                        RgbColor.Black, RgbColor.Gray, 1);
                    cell1.Align = ceTe.DynamicPDF.TextAlign.Center;
                    cell1.VAlign = VAlign.Center;
                    row2.Cells.Add(bloodStatistics[i].Aneg.ToString());

                    Row2 row3 = table.Rows.Add(20);
                    Cell2 cell2 = row3.Cells.Add("B positive", Font.HelveticaBold, 12,
                        RgbColor.Black, RgbColor.Gray, 1);
                    cell2.Align = ceTe.DynamicPDF.TextAlign.Center;
                    cell2.VAlign = VAlign.Center;
                    row3.Cells.Add(bloodStatistics[i].Bpos.ToString());

                    Row2 row4 = table.Rows.Add(20);
                    Cell2 cell3 = row4.Cells.Add("B negative", Font.HelveticaBold, 12,
                        RgbColor.Black, RgbColor.Gray, 1);
                    cell3.Align = ceTe.DynamicPDF.TextAlign.Center;
                    cell3.VAlign = VAlign.Center;
                    row4.Cells.Add(bloodStatistics[i].Bneg.ToString());

                    Row2 row5 = table.Rows.Add(20);
                    Cell2 cell4 = row5.Cells.Add("AB positive", Font.HelveticaBold, 12,
                        RgbColor.Black, RgbColor.Gray, 1);
                    cell4.Align = ceTe.DynamicPDF.TextAlign.Center;
                    cell4.VAlign = VAlign.Center;
                    row5.Cells.Add(bloodStatistics[i].ABpos.ToString());

                    Row2 row6 = table.Rows.Add(20);
                    Cell2 cell5 = row6.Cells.Add("AB negative", Font.HelveticaBold, 12,
                        RgbColor.Black, RgbColor.Gray, 1);
                    cell5.Align = ceTe.DynamicPDF.TextAlign.Center;
                    cell5.VAlign = VAlign.Center;
                    row6.Cells.Add(bloodStatistics[i].ABneg.ToString());

                    Row2 row7 = table.Rows.Add(20);
                    Cell2 cell6 = row7.Cells.Add("O positive", Font.HelveticaBold, 12,
                        RgbColor.Black, RgbColor.Gray, 1);
                    cell6.Align = ceTe.DynamicPDF.TextAlign.Center;
                    cell6.VAlign = VAlign.Center;
                    row7.Cells.Add(bloodStatistics[i].Opos.ToString());

                    Row2 row8 = table.Rows.Add(20);
                    Cell2 cell7 = row8.Cells.Add("O negative", Font.HelveticaBold, 12,
                        RgbColor.Black, RgbColor.Gray, 1);
                    cell7.Align = ceTe.DynamicPDF.TextAlign.Center;
                    cell7.VAlign = VAlign.Center;
                    row8.Cells.Add(bloodStatistics[i].Oneg.ToString());

                    // Add the table to the page
                    page.Elements.Add(table);
                }

                page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
                document.Pages.Add(page);

                labelText = "Report charts";
                label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, ceTe.DynamicPDF.TextAlign.Center);
                page.Elements.Add(label);

                Chart chart = new Chart(0, 50, 550, 230);
                PlotArea plotArea = chart.PrimaryPlotArea;

                Title title1 = new Title("Blood Consumption");
                chart.HeaderTitles.Add(title1);

                IndexedColumnSeries columnSeries1 = new IndexedColumnSeries(_bloodBankService.GetById(bloodStatistics[0].BloodBankID).Name);
                columnSeries1.Values.Add(new float[] { bloodStatistics[0].Apos, bloodStatistics[0].Aneg, bloodStatistics[0].Bpos, bloodStatistics[0].Bneg, bloodStatistics[0].ABpos, bloodStatistics[0].ABneg, bloodStatistics[0].Opos, bloodStatistics[0].Oneg });
                AutoGradient autogradient1 = new AutoGradient(180f, CmykColor.Red, CmykColor.IndianRed);
                columnSeries1.Color = autogradient1;
                plotArea.Series.Add(columnSeries1);
                for (int i=1; i<bloodStatistics.Count; i++)
                {
                     
                        IndexedColumnSeries columnSeries2 = new IndexedColumnSeries(_bloodBankService.GetById(bloodStatistics[i].BloodBankID).Name);
                        columnSeries2.Values.Add(new float[] { bloodStatistics[i].Apos, bloodStatistics[i].Aneg, bloodStatistics[i].Bpos, bloodStatistics[i].Bneg, bloodStatistics[i].ABpos, bloodStatistics[i].ABneg, bloodStatistics[i].Opos, bloodStatistics[i].Oneg });
                        Random rnd = new Random();
                        int num = rnd.Next(0, 6); 
                         AutoGradient autogradient2 = new AutoGradient(180f, new CmykColor( 0.1f + num/10, 0.2f + num / 10, 0.3f + num / 10, 0.0f), new CmykColor(0.2f + num / 10, 0.3f + num / 10, 0.4f + num / 10, 0.0f));
                        columnSeries2.Color = autogradient2;                      
                        plotArea.Series.Add(columnSeries2);                      
                }


                Title lTitle = new Title("Blood Amount (in units)");
                columnSeries1.YAxis.Titles.Add(lTitle);

                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("APos", 0));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("ANeg", 1));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("BPos", 2));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("BNeg", 3));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("ABPos", 4));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("ABNeg", 5));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("OPos", 6));
                columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("ONeg", 7));

               page.Elements.Add(chart);

                
                // Create a chart
                Chart chart1 = new Chart(0, 350, 400, 400);
                // Add a plot area to the chart
                PlotArea plotArea1 = chart1.PlotAreas.Add(20, 20, 350, 350);

                // Create the Header titles and add it to the chart
                Title tTitle = new Title("Total amount of blood (in units)");    
                chart1.HeaderTitles.Add(tTitle);


                // Create autogradient colors
                autogradient1 = new AutoGradient(90f, CmykColor.Red, CmykColor.IndianRed);
                AutoGradient autogradient3 = new AutoGradient(90f, CmykColor.Green, CmykColor.YellowGreen);
                AutoGradient autogradient4 = new AutoGradient(90f, CmykColor.Blue, CmykColor.LightBlue);
                AutoGradient autogradient5 = new AutoGradient(90f, CmykColor.Salmon, CmykColor.LightSalmon);
                AutoGradient autogradient6 = new AutoGradient(90f, CmykColor.Coral, CmykColor.LightCoral);
                AutoGradient autogradient7 = new AutoGradient(90f, CmykColor.Bisque, CmykColor.Cyan);
                AutoGradient autogradient8 = new AutoGradient(90f, CmykColor.Aquamarine, CmykColor.MediumAquaMarine);
                AutoGradient autogradient9 = new AutoGradient(90f, CmykColor.PaleVioletRed, CmykColor.PaleGreen);


                // Create a scalar datalabel
                ScalarDataLabel da = new ScalarDataLabel(true, false, false);
                // Create a pie series 
                PieSeries pieSeries = new PieSeries();
                // Set scalar datalabel to the pie series
                pieSeries.DataLabel = da;
                // Add series to the plot area
                plotArea1.Series.Add(pieSeries);

                //Add pie series elements to the pie series
                pieSeries.Elements.Add(bloodStatistics.Sum(x=>x.Apos), "A Positive");
                pieSeries.Elements.Add(bloodStatistics.Sum(x => x.Aneg), "A Negative");
                pieSeries.Elements.Add(bloodStatistics.Sum(x => x.Bpos), "B Positive");
                pieSeries.Elements.Add(bloodStatistics.Sum(x => x.Bneg), "B Negative");
                pieSeries.Elements.Add(bloodStatistics.Sum(x => x.ABpos), "AB Positive");
                pieSeries.Elements.Add(bloodStatistics.Sum(x => x.ABneg), "AB Negative");
                pieSeries.Elements.Add(bloodStatistics.Sum(x => x.Opos), "O Positive");
                pieSeries.Elements.Add(bloodStatistics.Sum(x => x.Oneg), "O Negative");


                // Assign autogradient colors to series elements
                if (pieSeries.Elements.Count > 0)
                    pieSeries.Elements[0].Color = autogradient1;
                if (pieSeries.Elements.Count > 1)
                    pieSeries.Elements[1].Color = autogradient3;
                if (pieSeries.Elements.Count > 2)
                    pieSeries.Elements[2].Color = autogradient4;
                if (pieSeries.Elements.Count > 3)
                    pieSeries.Elements[3].Color = autogradient5;
                if (pieSeries.Elements.Count > 4)
                    pieSeries.Elements[4].Color = autogradient6;
                if (pieSeries.Elements.Count > 5)
                    pieSeries.Elements[5].Color = autogradient7;
                if (pieSeries.Elements.Count > 6)
                    pieSeries.Elements[6].Color = autogradient8;
                if (pieSeries.Elements.Count > 7)
                    pieSeries.Elements[7].Color = autogradient9;

                // Add the chart to the page
                page.Elements.Add(chart1);
            } 
        




            MemoryStream stream = new MemoryStream();
            document.Draw(stream);
            //Close the document.
            byte[] docBytes = stream.ToArray();

            return docBytes;

        }

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
            //byte[] paramFileBytes = CreateDocumentInRange(_bloodStatisticService.getTenderStatistic(new DateRange(new DateTime(2023,1,1), new DateTime(2023, 3, 20))));

            BloodBank.BloodBank bloodBank = _bloodBankRepository.GetByName(bankName);
            string text = "Dear Sir or Madam, in the attachment, we are send you a report for the blood bank <strong>"+bankName+"</strong> for the last "+generatePeriod+" days.";
            
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
