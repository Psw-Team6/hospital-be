using System;
using System.Collections.Generic;
using System.Net;
using HospitalLibrary.CustomException;

namespace HospitalAPI.Exceptions
{
    public static class ExceptionStatusCodes
    {
        private static Dictionary<Type, HttpStatusCode> _exceptionStatusCodes = new Dictionary<Type, HttpStatusCode>()
        {
            {typeof(DoctorException), HttpStatusCode.BadRequest}
        };

        public static HttpStatusCode GetExceptionStatusCode(Exception exception)
        {
            var exFound = _exceptionStatusCodes.TryGetValue(exception.GetType(), out var statusCode);
            return exFound ? statusCode : HttpStatusCode.InternalServerError;
        }
    }
}