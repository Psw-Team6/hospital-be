using System;

namespace HospitalLibrary.Patients.Exceptions
{
    public class PatientNotFound:Exception
    {
        public PatientNotFound(string message) : base(message)
        {
        }
    }
}