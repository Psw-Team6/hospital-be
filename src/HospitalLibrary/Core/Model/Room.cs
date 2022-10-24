using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Appointments.Model;

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
        public IEnumerable<Appointment> Appointments { get; set; }

    }
}
