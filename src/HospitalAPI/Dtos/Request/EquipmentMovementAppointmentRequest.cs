using System;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class EquipmentMovementAppointmentRequest
    {
        public Guid EquipmentId { get; set; }
        
        public int Amount { get; set; }
        
        public Guid OriginalRoomId { get; set; }
        
        public Guid DestinationRoomId { get; set; }

        public DateRange DatesForSearch { get; set; }

        public TimeSpan Duration{ get; set; }
        public string EquipmentName { get; set; }
    }
}