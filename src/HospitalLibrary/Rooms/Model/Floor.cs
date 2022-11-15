using System;
using System.Collections.Generic;

namespace HospitalLibrary.Rooms.Model
{
    public class Floor
    {
        public Guid Id { get; set; }
        public int FloorNumber { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<Room> Rooms { get; set; }
        
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
        
       /* public Guid FloorPlanViewId { get; set; }
        public FloorPlanView FloorPlanView { get; set; }*/
    }
}