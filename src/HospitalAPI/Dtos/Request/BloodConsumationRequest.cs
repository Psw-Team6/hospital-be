using System;
using HospitalLibrary.BloodUnits.Model;

namespace HospitalAPI.Dtos.Request
{
    public class BloodConsumationRequest
    {
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public String Purpose { get; set; }
        public Guid BloodUnitId { get; set; }
    }
    
}