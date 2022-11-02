using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.CustomException
{
    public class PatientException:Exception
    {
        public PatientException()
        {
        }

        protected PatientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PatientException(string message) : base(message)
        {
        }

        public PatientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}