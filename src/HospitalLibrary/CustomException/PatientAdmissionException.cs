using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.CustomException
{
    public class PatientAdmissionException : Exception
    {
        public PatientAdmissionException()
        {
        }

        protected PatientAdmissionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PatientAdmissionException(string message) : base(message)
        {
        }

        public PatientAdmissionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
   
}