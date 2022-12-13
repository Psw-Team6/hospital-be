using System.Collections.Generic;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class ConsiliumRequest
    {
        public string Theme { get; set; }
        public IEnumerable<DoctorConsiliumResponse> Doctors { get; set; }
        public TimeRange TimeRange { get; set; }
    }
}