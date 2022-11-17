using System;

namespace HospitalAPI.Dtos.Request
{
    public class RoomEquipmentRequest
    {
        public Guid RoomEquipmentId { get; set; }
        public int Amount { get; set; }
        
        public String EquipmentName { get; set; }
        
        public Guid RoomId { get; set; }
    }
}