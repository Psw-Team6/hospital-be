using System;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.EquipmentMovement.Model
{
    public class EquipmentMovementRequest
    {
        public Guid Id { get; set; }
        
        public int Amount { get; set; }
        
        public Guid OriginalRoomId { get; set; }
        public Room OriginalRoom { get; set; }
        
        public Guid DestinationRoomId { get; set; }
        public Room DestinationRoom { get; set; }

        public DateRange DatesForSearch { get; set; }

        public TimeSpan Duration{ get; set; }
        
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
    }
}