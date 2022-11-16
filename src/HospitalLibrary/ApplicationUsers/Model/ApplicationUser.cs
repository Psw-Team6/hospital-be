using System;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.ApplicationUsers.Model
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg{ get; set; }
        public string Phone{ get; set; }
        public UserRole UserRole { get; set; }
    }
}