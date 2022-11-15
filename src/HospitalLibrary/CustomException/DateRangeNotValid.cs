using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.CustomException
{
    public class DateRangeNotValid: DateRangeException
    {
        protected DateRangeNotValid()
        {
        }

        protected DateRangeNotValid(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DateRangeNotValid(string message) : base(message)
        {
        }

        public DateRangeNotValid(string message, Exception innerException) : base(message, innerException)
        {
        }
        
    }
}