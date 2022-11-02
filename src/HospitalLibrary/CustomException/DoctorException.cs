using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace HospitalLibrary.CustomException
{
    public class DoctorException:Exception
    {
        protected DoctorException()
        {
        }

        protected DoctorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DoctorException(string message) : base(message)
        {
        }

        public DoctorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class DoctorNotExist : DoctorException
    {
        public DoctorNotExist(string message) : base(message)
        {
        }
    }
    public class DoctorIsNotAvailable : DoctorException
    {
        public DoctorIsNotAvailable(string message) : base(message)
        {
        }
    }
}