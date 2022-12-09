using System;
using System.Collections.Generic;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Consiliums.Model
{
    public class Consilium
    {
        public Guid Id { get; set; }
        public string Theme { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
        public DateRange DateRange { get; set; }
    }
}