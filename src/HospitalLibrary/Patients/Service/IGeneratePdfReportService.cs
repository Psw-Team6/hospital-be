using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalLibrary.Patients.Service
{
    public interface IGeneratePdfReportService
    {
        void GeneratePdfReport(PatientAdmission admission, TreatmentReport treatmentReport);
        string GenerateTextInPdf(PatientAdmission patientAdmission, TreatmentReport treatmentReport);
        byte[] GetAppointmentPdfReport(Examination examination);
    }
}