using System;
using System.Collections.Generic;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class ConsiliumResponse
    {
        public Guid Id { get; set; }
        public string Theme { get; set; }
        public IEnumerable<DoctorResponse> Doctors { get; set; }
        public DateRange DateRange { get; set; }
    }
}