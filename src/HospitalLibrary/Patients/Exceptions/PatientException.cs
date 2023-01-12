using System;

namespace HospitalLibrary.Patients.Exceptions
{
    public class PatientException:Exception
    {
        public PatientException(string message) : base(message)
        {
        }
    }
}