using System;

namespace HospitalLibrary.Patients.Exceptions
{
    public class BloodPressureException:PatientException
    {
        public BloodPressureException(string message) : base(message)
        {
        }
    }
}