using System.Collections.Generic;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Medicines.Model;

namespace HospitalAPI.Dtos.Request
{
    public class ExaminationPrescriptionRequest
    {
        public string Usage { get; set; }
        public List<MedicineExaminationResponse> Medicines { get; set; } 
    }
}