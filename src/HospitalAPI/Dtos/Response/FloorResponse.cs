using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalAPI.Dtos.Response
{
    public class FloorResponse
    {
        public Guid Id { get; set; }
        public int FloorNumber { get; set; }
        public string Name { get; set; }
        
        
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}