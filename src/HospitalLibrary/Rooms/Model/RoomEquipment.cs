using System;
using System.Collections.Generic;
using HospitalLibrary.Enums;

namespace HospitalLibrary.Rooms.Model
{
    public class RoomEquipment
    {
        public Guid RoomEquipmentId { get; set; }
        public int Amount { get; set; }
        
        public string EquipmentName { get; set; }
        
        public Guid RoomId { get; set; }
        
       // public Room Room { get; set; } 
        
        //public List<Equipment> Equipment { get; set; }
    }
}