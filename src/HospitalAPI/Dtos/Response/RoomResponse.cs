using System;
using HospitalLibrary.Core.Model;

namespace HospitalAPI.Dtos.Response
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BuildingId { get; set; }
        public Guid FloorId { get; set; }
        public Guid GRoomId { get; set; }
    }
}