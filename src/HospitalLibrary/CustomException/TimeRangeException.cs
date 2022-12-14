using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.CustomException
{
    public class TimeRangeException: Exception
    {
        public TimeRangeException()
        {
        }

        protected TimeRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TimeRangeException(string message) : base(message)
        {
        }

        public TimeRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}