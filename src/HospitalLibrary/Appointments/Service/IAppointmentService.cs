using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Repository;

namespace HospitalLibrary.Appointments.Service
{
    public interface IAppointmentService
    {
        public Task<List<Appointment>> GetAllByRoomId(Guid id);
    }
}