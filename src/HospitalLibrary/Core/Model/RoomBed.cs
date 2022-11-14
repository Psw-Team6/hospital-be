using System;

namespace HospitalLibrary.Core.Model
{
    public class RoomBed
    {
        public Guid Id { get; set; }
        public bool IsFree { get; set; }
        public string Number { get; set; }
        public Room Room { get; set; }
        public Guid RoomId { get; set; }
    }
}