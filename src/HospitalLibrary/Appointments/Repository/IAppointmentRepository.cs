using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;

namespace HospitalLibrary.Appointments.Repository
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsForDoctor(Guid doctorId);
        Task<IEnumerable<Appointment>> GetAllAppointmentsForPatient(Guid patientId);
        Task<List<Appointment>> GetAppointmentsForExamination(Guid doctorId);
        //Task<List<Appointment>> GetNextAppointments(Guid doctorId);
        Task<Appointment> GetAppointmentsById(Guid appointmentId);
    }
}