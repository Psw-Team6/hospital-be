using System.Collections.Generic;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class ConsiliumRequest
    {
        public string Theme { get; set; }
        public IEnumerable<DoctorResponse> Doctors { get; set; }
        public DateRange DateRange { get; set; }
    }
}