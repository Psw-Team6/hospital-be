
using System;

namespace HospitalAPI.Dtos.Request
{
    public class RoomRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BuildingId { get; set; }
        public Guid FloorId { get; set; }
        public Guid GRoomId { get; set; }
    }
}