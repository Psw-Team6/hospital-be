using System;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class RoomMergingRequest
    {
        public Guid Room1Id { get; set; }

        public Guid Room2Id { get; set; }

        public DateRange DatesForSearch { get; set; }

        public TimeSpan Duration{ get; set; }
    }
}