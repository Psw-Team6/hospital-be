using System;

namespace HospitalAPI.Dtos.Request
{
    public class AppointmentReportPdfRequest
    {
        public Boolean Anonymized { get; set; }
        public Boolean Presciptions { get; set; }
        public Boolean Symptoms { get; set; }
    }
}