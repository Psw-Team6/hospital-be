using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class PatientProfileRequest
    {
        public string Username { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public Doctor Doctor { get; set; }
    }
}