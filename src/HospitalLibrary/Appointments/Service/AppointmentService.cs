using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;

namespace HospitalLibrary.Appointments.Service
{
    public class AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            var appointments =  await _unitOfWork.AppointmentRepository.GetAllAsync();
            return appointments;
        }

        public async Task<bool> RescheduleAppointment(Appointment appointment)
        {
            // var oldAppointment = await GetById(appointment.Id);
            // if (oldAppointment == null)
            // {
            //     return false;
            // }
            //
            // await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
            // await _unitOfWork.CompleteAsync();
            // return true;

            await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.CompleteAsync();
            return true;
            
            
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
           //Task.FromException(new Exception("aaa"));
            var appointmentCreated = await _unitOfWork.AppointmentRepository.CreateAsync(appointment);
            await _unitOfWork.CompleteAsync();
            return appointmentCreated;
        }

        public async Task<Appointment> GetById(Guid id)
        {
            return await _unitOfWork.AppointmentRepository.GetByIdAsync(id);
        }

        public async Task<List<Appointment>> GetDoctorAppointments(Guid id)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAsync();
            var doctorAppointments = appointments.Where(x => x.DoctorId == id);
            return doctorAppointments.ToList();
        }
    }
}