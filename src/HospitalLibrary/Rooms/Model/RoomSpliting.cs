using System;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Rooms.Model
{
    public class RoomSpliting
    {
        
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public DateRange DatesForSearch { get; set; }
        public TimeSpan Duration{ get; set; }
        public string newRoomName { get; set; }
    }
}