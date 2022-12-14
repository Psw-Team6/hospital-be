using System;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class RoomSplitingRequest
    {
        public DateRange DatesForSearch { get; set; }
        public TimeSpan Duration{ get; set; }
        public string newRoomName { get; set; }
    }
}