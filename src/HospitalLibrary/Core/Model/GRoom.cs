using System;

namespace HospitalLibrary.Core.Model
{
    public class GRoom
    {
        public Guid Id { get; set; }
        public  int PositionX { get; set; }
        
        public  int PositionY { get; set; }
        
        public  int Lenght { get; set; }
        
        public  int Width { get; set; }
        public Guid RoomId { get; set; }//add one to one mapping with room
    }
}