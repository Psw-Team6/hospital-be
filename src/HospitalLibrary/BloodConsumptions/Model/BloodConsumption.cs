using System;
using HospitalLibrary.BloodUnits.Model;

namespace HospitalLibrary.BloodConsumptions.Model
{
    public class BloodConsumption
    {
        public Guid Id { get; set; }
        public Guid BloodUnitId { get; set; }
        public BloodUnit BloodUnit { get; set; }
        public int Amount { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime Date { get; set; }
        public String Purpose { get; set; }
        
    }
}