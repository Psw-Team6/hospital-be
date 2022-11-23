using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.TreatmentReports.Model;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;

namespace HospitalLibrary.Patients.Service
{
    public class GeneratePdfReportService
    {
        public void GeneratePdfReport(PatientAdmission admission,TreatmentReport treatmentReport)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 20, XFontStyle.Bold);
            gfx.DrawString("Patient admission report", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);  
            var secondPage = document.AddPage();
            var gfxBody = XGraphics.FromPdfPage(secondPage);
            var formatterBody = new XTextFormatter(gfxBody);
            var layoutRectangleBody = new XRect(10, 10, page.Width, page.Height);
            var fontBody = new XFont("Verdana", 14, XFontStyle.Regular);
            formatterBody.DrawString( GenerateTextInPdf(admission, treatmentReport), fontBody, XBrushes.Black,layoutRectangleBody);
            var filename = "PDFDocument.pdf";
            document.Save(filename);
            Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
        }

        private  string GenerateTextInPdf(PatientAdmission patientAdmission,TreatmentReport treatmentReport)
        {
           
            var sb = new StringBuilder();
            sb.AppendLine("Patient : " + patientAdmission.Patient.Name + " " + patientAdmission.Patient.Surname).AppendLine(Environment.NewLine);
            sb.AppendLine("Reason of hospitalizing : " + patientAdmission.Reason).AppendLine(Environment.NewLine);
            sb.AppendLine("Reason of discharge : " + patientAdmission.ReasonOfDischarge).AppendLine(Environment.NewLine);
            if (treatmentReport.MedicinePrescriptions.Any())
            {        
                sb.Append("Medicine prescriptions : ");
                treatmentReport.MedicinePrescriptions.ToList().ForEach(x => sb.Append(x.Description + ", "));
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine(Environment.NewLine);
            }  
            if (treatmentReport.BloodPrescriptions.Any())
            {        
                sb.Append("Blood prescriptions : ");
                treatmentReport.BloodPrescriptions.ToList().ForEach(x => sb.Append(x.Description + ", "));
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}