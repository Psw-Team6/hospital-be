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
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid PatientID { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public Guid DoctorId { get; set; }
        public AppointmentState AppointmentState { get; set; }
        
    }
}