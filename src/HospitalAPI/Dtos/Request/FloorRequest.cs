using System;

namespace HospitalAPI.Dtos.Request
{
    public class FloorRequest
    {
        public Guid Id { get; set; }
        public int FloorNumber { get; set; }
        public string Name { get; set; }
        public Guid BuildingId { get; set; }
    }
}