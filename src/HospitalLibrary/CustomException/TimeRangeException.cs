using System;

namespace HospitalLibrary.CustomException
{
    public class TimeRangeException: Exception
    {
        public TimeRangeException(string message) : base(message)
        {
        }
    }
}