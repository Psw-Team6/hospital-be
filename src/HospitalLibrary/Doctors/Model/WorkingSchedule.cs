using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HospitalLibrary.Doctors.Model
{
    public class WorkingSchuedule
    {
        public List<WorkingDay> WorkingDays{ get; set; }
        public Doctor Doctor{ get; set; }
        public DateTime CompletionDate { get; set; }
        
    }
}