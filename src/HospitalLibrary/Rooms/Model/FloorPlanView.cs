using System;

namespace HospitalLibrary.Rooms.Model
{
    public class FloorPlanView
    {
        public Guid Id { get; set; }
        
        public  int PosX { get; set; }
        
        public  int PosY { get; set; }
        
        public  int Lenght { get; set; }
        
        public  int Width { get; set; }
    }
}