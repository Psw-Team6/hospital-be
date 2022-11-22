using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.CustomException
{
    public class PatientDischargeException : Exception
    {
        public PatientDischargeException()
        {
        }

        protected PatientDischargeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PatientDischargeException(string message) : base(message)
        {
        }

        public PatientDischargeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}