using System;

namespace HospitalLibrary.Examinations.Exceptions
{
    public class ExaminationNotFoundException:Exception
    {
        public ExaminationNotFoundException(string message) : base(message)
        {
        }
    }
}