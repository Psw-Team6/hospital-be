using System;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Appointments.Model
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public bool Emergent { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public Patient Patient { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public Doctor Doctor { get; set; }
        public AppointmentState AppointmentState { get; set; }
    }
    
}