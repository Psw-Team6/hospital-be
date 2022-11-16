using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Floor
    {
        public Guid Id { get; set; }
        public int FloorNumber { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<Room> Rooms { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}