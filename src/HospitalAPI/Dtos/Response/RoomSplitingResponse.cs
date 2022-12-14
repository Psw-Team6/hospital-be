using System;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class RoomSplitingResponse
    {
        public Guid Id { get; set; }
        public DateRange DatesForSearch { get; set; }
        public string newRoomName { get; set; }

    }
}