using System;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class PatientResponseName
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone{ get; set; }
    }
}