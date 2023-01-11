using System;
using System.Collections.Generic;
using HospitalLibrary.Common;

namespace HospitalLibrary.Patients.Model
{
    public class PatientHealthStateNotification:Entity<Guid>
    {
        public Patient Patient { get; private set; }
        public List<string> Notifications { get;private set;}

        public PatientHealthStateNotification(Patient patient, List<string> notifications)
        {
            Patient = patient;
            Notifications = notifications;
        }

        public PatientHealthStateNotification()
        {
        }
    }
}