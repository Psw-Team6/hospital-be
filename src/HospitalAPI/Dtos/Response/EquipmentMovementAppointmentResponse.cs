using System;

namespace HospitalAPI.Dtos.Response
{
    public class EquipmentMovementAppointmentResponse
    {
        public Guid Id { get; set; }
        public Guid RoomOfOrigin { get; set; }
        public Guid RoomOfDestination { get; set; }
        public int Amount { get; set; }
        
    }
}