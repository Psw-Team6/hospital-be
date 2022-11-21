using System;
using HospitalLibrary.BloodUnits.Model;

namespace HospitalLibrary.BloodConsumptions.Model
{
    public class BloodConsumptionCreateDto
    {
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public String Purpose { get; set; }
        public Guid doctorId { get; set; }
    }
}