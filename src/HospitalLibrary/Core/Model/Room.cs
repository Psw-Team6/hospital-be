using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Core.Model
{
    public class Room
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Number { get; set; }
        [Range(1, 10)]
        public int Floor { get; set; }
        public Doctor Doctor { get; set; }

    }
}
