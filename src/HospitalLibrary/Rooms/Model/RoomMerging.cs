using System;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Rooms.Model
{
    public class RoomMerging
    {
        public Guid Id { get; set; }
        public Guid Room1Id { get; set; }
        public Room Room1 { get; set; }

        public Guid Room2Id { get; set; }
        public Room Room2 { get; set; }

        public DateRange DatesForSearch { get; set; }

        public TimeSpan Duration{ get; set; }
    }
}