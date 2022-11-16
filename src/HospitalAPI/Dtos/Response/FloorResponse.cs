using System;
using System.Collections.Generic;
using HospitalLibrary.Rooms.Model;

namespace HospitalAPI.Dtos.Response
{
    public class FloorResponse
    {
        public Guid Id { get; set; }
        public int FloorNumber { get; set; }
        public string Name { get; set; }
        public Guid BuildingId { get; set; }
    }
}