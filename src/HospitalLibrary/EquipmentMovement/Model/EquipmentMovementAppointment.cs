using System;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.EquipmentMovement.Model
{
    public class EquipmentMovementAppointment
    {
        public Guid Id { get; set; }
        
        public int Amount { get; set; }
        
        public Guid OriginalRoomId { get; set; }
        public Room OriginalRoom { get; set; }
        
        public Guid DestinationRoomId { get; set; }
        public Room DestinationRoom { get; set; }

        public DateRange Duration { get; set; }

        public string EquipmentName { get; set; }
    }
}