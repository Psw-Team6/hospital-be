using System;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Dtos.Request
{
    public class AppointmentRequest
    {
        public bool Emergent { get; set; }
        public DateRange Duration { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public AppointmentState AppointmentState { get; set; }
    }
}