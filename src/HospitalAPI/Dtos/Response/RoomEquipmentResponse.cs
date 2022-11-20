using System;

namespace HospitalAPI.Dtos.Response
{
    public class RoomEquipmentResponse
    {
        public Guid RoomEquipmentId { get; set; }
        public int Amount { get; set; }
        
        public string EquipmentName { get; set; }
        
        public Guid RoomId { get; set; }
    }
}