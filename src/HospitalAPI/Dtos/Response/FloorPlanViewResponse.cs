using System;

namespace HospitalAPI.Dtos.Response
{
    public class FloorPlanViewResponse
    {
        
        public Guid Id { get; set; }
        
        public  int PosX { get; set; }
        
        public  int PosY { get; set; }
        
        public  int Lenght { get; set; }
        
        public  int Width { get; set; }
    }
}