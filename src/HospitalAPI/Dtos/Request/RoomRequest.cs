using System;

namespace HospitalAPI.Dtos.Request
{
    public class RoomRequest
    {
        public string Number { get; set; }
        public int Floor { get; set; }
    }
}