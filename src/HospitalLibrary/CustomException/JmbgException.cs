using System;

namespace HospitalLibrary.CustomException
{
    public class JmbgException:Exception
    {
        public JmbgException(string message) : base(message)
        {
        }
    }
}