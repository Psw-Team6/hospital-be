using System;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Response
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }
        public bool Emergent { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public Patient Patient { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public Doctor Doctor { get; set; }
        public AppointmentState AppointmentState { get; set; }
        
    }
}