using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Doctors.Model
{
    public class Specialization
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
    }
}