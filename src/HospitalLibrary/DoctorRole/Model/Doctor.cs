using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.DoctorRole.Model
{
    public class Doctor:ApplicationUser
    {
        
        public Guid Id { get; set; }
        public Guid SpecializationId { get; set; }
        public Specialization Specialization{ get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }

    }
}