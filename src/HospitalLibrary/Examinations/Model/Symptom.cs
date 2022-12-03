using System;
using System.Collections.Generic;

namespace HospitalLibrary.Examinations.Model
{
    public class Symptom
    {
        public  Guid Id { get; set; }
        public string Description { get; set; }
        public List<Examination> Examinations { get; private set; }
    }
}