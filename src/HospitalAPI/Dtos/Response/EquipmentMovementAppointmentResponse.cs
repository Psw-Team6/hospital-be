using System;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class EquipmentMovementAppointmentResponse
    {
        public Guid Id { get; set; }
        public Guid OriginalRoomId { get; set; }
        public Guid DestinationRoomId { get; set; }
        public int Amount { get; set; }
        public DateRange Duration { get; set; }
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        
    }
}