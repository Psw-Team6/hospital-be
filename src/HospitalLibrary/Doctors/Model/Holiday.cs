﻿using System;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Doctors.Model
{
    public class Holiday
    {
        public Guid Id { get; set; }
        public Doctor Doctor { get; set; }
        public DateRange DateRange { get; set;}
        
        public String Description { get; set; }
        
        public Boolean isUrgent { get; set; }
    }
}