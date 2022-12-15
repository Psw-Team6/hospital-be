using HospitalLibrary.Enums;
using HospitalLibrary.Patients.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Feedbacks.Model


{

    public class Feedback
    {
        public Guid Id { get; private set; }
        [Required]
        [MinLength(3)]

        public Guid patientId { get; private set; }

        public Patient patient { get; private set; }

        public DateTime date { get; private set; }
       
        public String text { get; private set; }
        
        public Boolean isAnonymous { get; private set; }
        
        public Boolean isPublic { get; private set; }
        
        public Status status { get; private set; }
        


    }
}
