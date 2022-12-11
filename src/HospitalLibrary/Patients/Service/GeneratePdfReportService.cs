using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.TreatmentReports.Model;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace HospitalLibrary.Patients.Service
{
    public class GeneratePdfReportService : IGeneratePdfReportService
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

        public  string GenerateTextInPdf(PatientAdmission patientAdmission,TreatmentReport treatmentReport)
        {
           
            var sb = new StringBuilder();
            sb.AppendLine("Patient : " + patientAdmission.Patient.Name + " " + patientAdmission.Patient.Surname).AppendLine(Environment.NewLine);
            sb.AppendLine("Reason of hospitalizing : " + patientAdmission.Reason).AppendLine(Environment.NewLine);
            sb.AppendLine("Reason of discharge : " + patientAdmission.ReasonOfDischarge).AppendLine(Environment.NewLine);
            if (treatmentReport.MedicinePrescriptions.Any())
            {        
                sb.Append("Medicine prescriptions : ");
                treatmentReport.MedicinePrescriptions.ToList().ForEach(x => sb.Append(x.Description + ", "));
                sb.Remove(sb.Length - 2, 1);
                sb.AppendLine(Environment.NewLine);
            }  
            if (treatmentReport.BloodPrescriptions.Any())
            {        
                sb.Append("Blood prescriptions : ");
                treatmentReport.BloodPrescriptions.ToList().ForEach(x => sb.Append(BloodTypeToString(x.BloodType) +"( " + x.Amount + " )" + ", "));
                sb.Remove(sb.Length - 2, 1);
                sb.AppendLine(Environment.NewLine);
            }
            return sb.ToString();
        }

        private string BloodTypeToString(BloodType type)
        {
            if (type == BloodType.Aneg)
            {
                return "A-";

            }
            else if (type == BloodType.Apos)
            {
                return "A+";

            }
            else if (type == BloodType.Bneg)
            {
                return "B-"; 
            }
            else if (type == BloodType.Bpos)
            {
                return "B+"; 
            }else if (type == BloodType.Oneg)
            {
                return "0-"; 
            }else if (type == BloodType.Opos)
            {
                return "0+"; 
            }else if (type == BloodType.ABneg)
            {
                return "AB-"; 
            }
            return "AB+"; 
        }

        private byte[] GeneratePDF(string content)
        {
            PdfDocument  document = new PdfDocument();
            PdfGenerator.AddPdfPages(document,content,PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            return response;
        }

        public byte[] GetAppointmentPdfReport(Examination examination)
        {
            string content = "<div style='width:100%; text-align:center'>";
            content += "<h2>Appointment report</h2>";
            content += "<h2>Date: "+examination.Appointment.Duration.From+"</h2></div> <div style='width:100%;'>";
            if (examination.Appointment.Patient != null)
            {
                content += "<div><h3>Patient</h3>";
                content +=  "<p>" +examination.Appointment.Patient.Name + " " +examination.Appointment.Patient.Surname+"</p>";
                content += "<p>Age: " + examination.Appointment.Patient.Age+ "yr</p>";
                content += "<p>Blood type: " + BloodTypeToString((BloodType)examination.Appointment.Patient.BloodType) + "</p></div>";
            }

            content += "<h3>Anamnesis</h3>";
            content += "<p>"+examination.Anamnesis+"</p>";
            
            if (examination.Symptoms!=null)
            {
                content += "<div><h3>Symptoms</h3><p>";
                foreach (var symptom in examination.Symptoms)
                {
                    content += symptom.Description+", ";
                }
                content += "</p></div>";
            }
            
            if (examination.Prescriptions!=null )
            {
                content += "<div><h3>Presciptions</h3>";
                content += "<table style='width:100%; border: 1px solid #000'>";
                content += "<thead style='font-weight:bold'>";
                content += "<tr>";
                content += "<td>Usage</td>";
                content += "<td>Medicine</td>";
                content += "</tr>";
                content += "</thead>";
                content += "<tbody>";
                foreach (var presription in examination.Prescriptions)
                {
                    content += "<tr>";
                    content += "<td>"+presription.Usage+"</td>";
                    var medicine = " ";
                    foreach (var med in presription.Medicines)
                    {
                        medicine += med.Name+", ";
                    }
                    content += "<td>"+medicine+"</td>";
                    content += "</tr>";
                }
                content += "</tbody>";
                content += "</table>";
                content += "</div>";
            }
            
            return GeneratePDF(content);
        }
        


    }
}