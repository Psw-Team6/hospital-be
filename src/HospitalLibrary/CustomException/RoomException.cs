using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.CustomException
{
    public class RoomException : Exception
    {
        public RoomException()
        {
        }

        protected RoomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RoomException(string message) : base(message)
        {
        }

        public RoomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}