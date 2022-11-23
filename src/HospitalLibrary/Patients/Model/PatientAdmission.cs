using System;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Patients.Model
{
    public class PatientAdmission
    {
        public Guid Id { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public Guid SelectedBedId  { get; set; }
        public RoomBed SelectedBed  { get; set; }
        public Guid SelectedRoomId { get; set;}
        public Room SelectedRoom { get; set;}
        public string Reason { get; set;}
        public string ReasonOfDischarge { get; set; }
        public DateTime? DateOfDischarge { get; set; }
        
        public void Update(string reasonOfDischarge, DateTime? dateOfDischarge)
        {
            ReasonOfDischarge = reasonOfDischarge;
            DateOfDischarge = dateOfDischarge;
        }
    }
}