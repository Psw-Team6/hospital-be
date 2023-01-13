using System;

namespace HospitalLibrary.CustomException
{
    public class PercentageException:Exception
    {
        public PercentageException(string message) : base(message)
        {
        }
    }
}