using System;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class AppointmentRequest
    {
        public bool Emergent { get; set; }
        public TimeSlot TimeSlot { get; set;}
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public AppointmentState AppointmentState { get; set; }
    }
}