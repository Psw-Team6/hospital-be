using System;

namespace HospitalLibrary.ApplicationUsers.Model
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}