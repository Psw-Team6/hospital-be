using System;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Patients.Model
{
    public class PatientAdmission
    {
        public Guid Id { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public Patient Patient { get; set; }
        public Guid PatientId { get; set; }
        public RoomBed SelectedBed  { get; set; }
        public Guid SelectedBedId  { get; set; }
        public Room SelectedRoom { get; set;}
        public Guid SelectedRoomId { get; set;}
        public string Reason { get; set;}
        public DateTime DateOfDischarge { get; set; }
    }
}