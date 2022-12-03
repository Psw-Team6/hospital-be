using System;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalLibrary.Prescriptions.Model
{
    public class BloodPrescription
    {
        public Guid Id { get; set; }
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public TreatmentReport TreatmentReport { get; set; }
        public Guid TreatmentReportId { get; set; }

    }
}