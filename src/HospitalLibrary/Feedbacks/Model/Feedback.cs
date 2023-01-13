using HospitalLibrary.Enums;
using HospitalLibrary.Patients.Model;
using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Common;

namespace HospitalLibrary.Feedbacks.Model


{

    public class Feedback:AbstractAggregate<Patient, Guid>
    {
        /*public Guid Id { get; private set; }*/
        [Required]
        [MinLength(3)]
        public Guid PatientId { get; private set; }

        /*
        public Patient Patient { get; private set; }*/

        public DateTime Date { get; private set; }
       
        public string Text { get; private set; }
        
        public bool IsAnonymous { get; private set; }
        
        public bool IsPublic { get; private set; }
        
        public Status Status { get; private set; }
        public Guid RootId { get; private set; }
        


    }
}
