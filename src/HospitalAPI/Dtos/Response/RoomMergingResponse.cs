using System;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class RoomMergingResponse
    {
        public Guid Id { get; set; }
        public DateRange DateRangeOfMerging { get; set; }
        
        public Guid Room1Id { get; set; }

        public Guid Room2Id { get; set; }
    }
}