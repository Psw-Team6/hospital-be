using System;
using System.Collections.Generic;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class ConsiliumResponse
    {
        public Guid Id { get; set; }
        public string Theme { get; set; }
        public IEnumerable<DoctorResponse> Doctors { get; set; }
        public TimeRange TimeRange { get; set; }
        public RoomBasicResponse Room { get; set; }
    }
}