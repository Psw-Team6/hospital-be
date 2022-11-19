using System;

namespace HospitalAPI.Dtos.Request
{
    public class BuildingRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}