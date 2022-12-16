using System;
using HospitalLibrary.SharedModel;

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
        public Jmbg Jmbg { get; set; }
        public Phone Phone { get; set; }
        public UserRole UserRole { get; set; }
        public bool Enabled { get; set; }
        public bool IsBlocked { get; set; }
    }
}