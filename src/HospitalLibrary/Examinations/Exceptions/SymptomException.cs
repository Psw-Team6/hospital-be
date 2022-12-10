using System;

namespace HospitalLibrary.Examinations.Exceptions
{
    public class SymptomException:Exception
    {
        public SymptomException(string message) : base(message)
        {
        }
    }
}