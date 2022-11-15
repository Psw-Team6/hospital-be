using System;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.BloodConsumptions.Model
{
    public class BloodConsumption
    {
        public Guid Id { get; set; }
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public String BloodBankName { get; set; }
        public Guid DoctorId { get; set; }
        //public Doctor Doctor { get; set; }
        public DateTime date { get; set; }
        public String purpose { get; set; }
    }
}