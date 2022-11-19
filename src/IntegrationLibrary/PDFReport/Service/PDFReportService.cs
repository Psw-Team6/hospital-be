using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf.Parsing;

namespace IntegrationLibrary.PDFReport.Service
{
    public class PDFReportService
    {
        public byte[] CreateDocument()
        {
            PdfDocument document = new PdfDocument();

            //Set the page size.
            document.PageSettings.Size = PdfPageSize.A4;

            //Add a page to the document.
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;

            //Set the font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            //Draw the text.
            graphics.DrawString("Hello World!!! NOVII", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 0));

            //Create file stream.
            using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Output.pdf"), FileMode.Create, FileAccess.ReadWrite))
            {
                //Save the PDF document to file stream.
                document.Save(outputFileStream);
            }

   
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Close the document.
            document.Close(true);
            byte[] docBytes = stream.ToArray();
            Console.WriteLine(docBytes.Length);
            return docBytes;
        }
    }
}
