using System.Collections.Generic;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class ConsiliumSpecializationRequest
    {
        public string Theme { get; set; }
        public IEnumerable<SpecializationResponse> Specializations { get; set; }
        public TimeRange TimeRange { get; set; }
    }
}