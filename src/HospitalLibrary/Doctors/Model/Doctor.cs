using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.ApplicationUsers;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Doctors.Model
{
    public class Doctor:ApplicationUser
    {
        public Guid SpecializationId { get; set; }
        public Specialization Specialization{ get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
        public Room Room { get; set; }
        public Guid RoomId { get; set; }
        public Guid WorkingScheduleId { get; set; }
        public WorkingSchedule WorkingSchedule { get; set; }
        

    }
}