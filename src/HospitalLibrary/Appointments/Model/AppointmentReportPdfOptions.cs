using System;

namespace HospitalLibrary.Appointments.Model
{
    public class AppointmentReportPdfOptions
    {
        public Boolean Anonymized { get; set; }
        public Boolean Presciptions { get; set; }
        public Boolean Symptoms { get; set; }
    }
}