using System;
using HospitalAPI.Dtos.Response;

namespace HospitalAPI.Dtos.Request
{
    public class MedicinePrescriptionRequest
    {
        public Guid TreatmentReportId { get; set; }
        public Guid MedicineId { get; set; }
    }
}