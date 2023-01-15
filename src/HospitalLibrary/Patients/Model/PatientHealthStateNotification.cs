using System;
using System.Collections.Generic;
using HospitalLibrary.Common;

namespace HospitalLibrary.Patients.Model
{
    public class PatientHealthStateNotification:Entity<Guid>
    {
        public Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        public List<string> Notifications { get;private set;}
        public DateTime ReceivedDate { get; private set; }

        public PatientHealthStateNotification(Patient patient, List<string> notifications, DateTime receivedDate)
        {
            Patient = patient;
            Notifications = notifications;
            ReceivedDate = receivedDate;
        }

        public PatientHealthStateNotification(Guid id, Patient patient, List<string> notifications, DateTime receivedDate) : base(id)
        {
            Patient = patient;
            Notifications = notifications;
            ReceivedDate = receivedDate;
        }

        public PatientHealthStateNotification(Guid id, Guid patientId, List<string> notifications, DateTime receivedDate) : base(id)
        {
            PatientId = patientId;
            Notifications = notifications;
            ReceivedDate = receivedDate;
        }

        public PatientHealthStateNotification()
        {
        }
    }
}