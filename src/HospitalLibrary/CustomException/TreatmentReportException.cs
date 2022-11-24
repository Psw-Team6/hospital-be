using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.CustomException
{
    public class TreatmentReportException: Exception
    {
        public TreatmentReportException()
        {
        }

        protected TreatmentReportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TreatmentReportException(string message) : base(message)
        {
        }

        public TreatmentReportException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}