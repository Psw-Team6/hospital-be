using System;
using System.Collections.Generic;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Rooms.Model
{
    public class RoomBed
    {
        public Guid Id { get; set; }
        public bool IsFree { get; set; }
        public string Number { get; set; }
        public Room Room { get; set; }
        public Guid RoomId { get; set; }
        public IEnumerable<PatientAdmission> Patients { get; set; }

        public void Update(bool isFree)
        {
            IsFree = isFree;
        }
    }
}