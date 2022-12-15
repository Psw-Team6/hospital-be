using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.CustomException
{
    public class ConsiliumException: Exception
    {
        public ConsiliumException()
        {
        }

        protected ConsiliumException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ConsiliumException(string message) : base(message)
        {
        }

        public ConsiliumException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    public class ThemeNotExist : ConsiliumException
    {
        public ThemeNotExist(string message) : base(message)
        {
        }
    } 
    public class ConsiliumDoctorsNotExist : ConsiliumException
    {
        public ConsiliumDoctorsNotExist(string message) : base(message)
        {
        }
    }
}