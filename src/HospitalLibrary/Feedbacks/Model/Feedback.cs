using HospitalLibrary.Enums;
using HospitalLibrary.Patients.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace HospitalLibrary.Feedbacks.Model


{
    public class Feedback
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]

        public Guid patientId { get; set; }

        public Patient patient { get; set; }

        public DateTime date { get; set; }
       
        public String text { get; set; }
        
        public Boolean isAnonymous { get; set; }
        
        public Boolean isPublic { get; set; }
        
        public Status status { get; set; }


    }
}
