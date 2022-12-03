using System;
using System.Collections.Generic;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Prescriptions.Model;

namespace HospitalLibrary.TreatmentReports.Model
{
    public class TreatmentReport
    {
        public Guid Id { get; set; }
        public Guid PatientAdmissionId { get; set; }
        
        public PatientAdmission PatientAdmission { get; set; }
        public IEnumerable<MedicinePrescription> MedicinePrescriptions { get; set; }
        public IEnumerable<BloodPrescription> BloodPrescriptions { get; set; }
        
    }
}