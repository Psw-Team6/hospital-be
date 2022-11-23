using System;
using HospitalLibrary.Enums;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Rooms.Model;

namespace HospitalAPI.Dtos.Response
{
    public class PatientAdmissionResponse
    {
        public Guid Id { get; set; }
        public PatientResponseName Patient { get; set; }
        public RoomResponse SelectedRoom { get; set; }
        public RoomBed SelectedBed { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public string Reason { get; set; }
        public DateTime? DateOfDischarge { get; set; }
    }
}