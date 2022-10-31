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
        private readonly IEmailService _emailService;

        public AppointmentService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
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
        
        public async Task<bool> CancelAppointment(Appointment appointment)
        {
            if (canCancleAppointment(appointment))
            {
                await _unitOfWork.AppointmentRepository.DeleteAsync(appointment);
                await _unitOfWork.CompleteAsync();
                await _emailService.SendCancelAppointmentEmail(appointment);
                return true;
            }

            return false;
        }
        
        public async Task<List<Appointment>> GetDoctorAppointments(Guid id)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAsync();
            var doctorAppointments = appointments.Where(x => x.DoctorId == id);
            return doctorAppointments.ToList();
        }
        
        
        private bool canCancleAppointment(Appointment appointment)
        {
            if(appointment.Duration.From.CompareTo(DateTime.Today.AddDays(-1)) < 0)
                return true;
            return false;
        }
    }
}