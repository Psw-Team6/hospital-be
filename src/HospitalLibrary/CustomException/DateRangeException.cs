using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.CustomException
{
    public class DateRangeException:Exception
    {
        public DateRangeException()
        {
        }

        protected DateRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DateRangeException(string message) : base(message)
        {
        }

        public DateRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}