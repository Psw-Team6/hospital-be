using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class PatientRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg { get; set; }
        public string Phone{ get; set; }
        public string DoctorId { get; set; }
        public int Age { get; set; }
        public BloodType BloodType { get; set; }
    }
}