using System;

namespace HospitalLibrary.sharedModel
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg{ get; set; }
        public string Phone{ get; set; }
    }
}