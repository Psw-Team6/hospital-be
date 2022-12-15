using System;

namespace HospitalLibrary.Patients.Model
{
    public class MaliciousPatient
    {
        public Guid Id { get; set; }
        
        public Guid PatientId { get; set; }
        
        public Patient Patient { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public int NumberOfCancellations { get; set; }
        
        public bool Malicious { get; set; }


    }
}