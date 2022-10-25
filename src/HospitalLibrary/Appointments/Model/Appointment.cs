using System;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Appointments.Model
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public bool Emergent { get; set; }
        public Patient Patient { get; private set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public AppointmentState AppointmentState { get; set; }
    }
    
}