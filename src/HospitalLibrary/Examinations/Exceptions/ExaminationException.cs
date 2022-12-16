using System;
using System.Runtime.Serialization;

namespace HospitalLibrary.Examinations.Exceptions
{
    public class ExaminationException:Exception
    {
        protected ExaminationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected ExaminationException(string message) : base(message)
        {
        }

        protected ExaminationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class ExaminationInvalidDate : ExaminationException
    {
        public ExaminationInvalidDate(string message) : base(message)
        {
        }
    }

    public class ExaminationInvalidAnamnesis : ExaminationException
    {
        public ExaminationInvalidAnamnesis(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ExaminationInvalidAnamnesis(string message) : base(message)
        {
        }

        public ExaminationInvalidAnamnesis(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    public class ExaminationPrescriptionException:ExaminationException
    {
        public ExaminationPrescriptionException(string message) : base(message)
        {
        }
    }

    public class AppointmentExaminationInvalidState : ExaminationException
    {
        public AppointmentExaminationInvalidState(string message) : base(message)
        {
        }
    }
}