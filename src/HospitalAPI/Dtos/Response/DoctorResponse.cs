using System;

namespace HospitalAPI.Dtos.Response
{
    public class DoctorResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg{ get; set; }
        public string Phone{ get; set; }
        public SpecializationResponse Specialization{ get; set; }
        public RoomResponse Room { get; set; }
    }
}