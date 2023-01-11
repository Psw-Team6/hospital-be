using System;

namespace HospitalAPI.Dtos.Request
{
    public class RoomEventRequest
    {
        public string EventName { get; set; }

        public string Value { get; set; }

        /// <summary>
        /// User that created event
        /// </summary>
        public string UserId { get; set; }
    }
}