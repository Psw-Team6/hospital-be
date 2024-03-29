using System;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class EquipmentMovementAppointmentRequest
    {
        
        public int Amount { get; set; }
        
        public DateRange DatesForSearch { get; set; }
        public Guid DestinationRoomId { get; set; }
        public TimeSpan Duration{ get; set; }
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public Guid OriginalRoomId { get; set; }
    }
}