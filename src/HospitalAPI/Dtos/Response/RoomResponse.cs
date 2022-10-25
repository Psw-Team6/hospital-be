using System;

namespace HospitalAPI.Dtos
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public int Floor { get; set; }
        
    }
}