using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Core.Model
{
    public class Building
    
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public IEnumerable<Floor> Floors { get; set; }
        
        public Guid FloorPlanViewId { get; set; }
        public FloorPlanView FloorPlanView { get; set; }
    }
}