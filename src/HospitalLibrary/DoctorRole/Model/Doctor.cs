using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.DoctorRole.Model
{
    public class Doctor:ApplicationUser
    {
        
        public Guid Id { get; set; }
        public Guid SpecializationId { get; set; }
        public Specialization Specialization{ get; set; }
        
        
    }
}