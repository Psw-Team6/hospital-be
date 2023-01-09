using System;

namespace HospitalLibrary.Rooms.Model
{
    public class RoomEvent
    {
        
        public Guid Id { get; set; }
        
        public string EventName { get; set; }
        
        public string Value { get; set; }
        
        /// <summary>
        /// TimeStamp of EventCreation
        /// </summary>
        public DateTime TimeStamp { get; set; }
        
        /// <summary>
        /// User that created event
        /// </summary>
        public Guid UserId { get; set; }
    }
}