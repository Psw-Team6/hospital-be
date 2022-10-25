using System.Collections.Generic;

namespace HospitalAPI.Validations.ErrorCodes
{
    public class ErrorResponse
    {
        public List<ErrorCode> Errors { get; set; } = new();
        
    }
}