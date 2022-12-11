using System;
using HospitalLibrary.Enums;

namespace HospitalAPI.Dtos.Response
{
    public class RoomBasicResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }
    }
}