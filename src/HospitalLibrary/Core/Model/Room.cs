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
        
        public Doctor Doctor { get; set; }
        
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
        
        public Guid FloorId { get; set; }
        public Floor Floor { get; set; }

        /*public Guid FloorPlanViewId { get; set; }
        public FloorPlanView FloorPlanView { get; set; }*/

    }
}
