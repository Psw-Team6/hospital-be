using System;
using System.Collections.Generic;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.TreatmentReports.Model
{
    public class TreatmentReport
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateRange DateRange { get; set; }
        public IEnumerable<MedicinePrescription> MedicinePrescriptions { get; set; }
        public IEnumerable<BloodPrescription> BloodPrescriptions { get; set; }
        public string ReasonOfHospitalization { get; set; }
        public string ReasonOfDischarge { get; set; }
    }
}