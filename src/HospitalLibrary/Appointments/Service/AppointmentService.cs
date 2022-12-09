using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;

namespace HospitalLibrary.Appointments.Service
{
    public class AppointmentService: IAppointmentService
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
            var appointments =  await _unitOfWork.GetRepository<AppointmentRepository>().GetAllAsync();
            return appointments;
        }
        

        public async Task<Appointment> GetById(Guid id)
        {
            return await _unitOfWork.GetRepository<AppointmentRepository>().GetByIdAsync(id);
        }
        
        public async Task<List<Appointment>> GetAllByRoomId(Guid id)
        {
            return await _unitOfWork.GetRepository<AppointmentRepository>().GetAllAppointmentsForRoom(id);
        }
        
        public async Task<bool> CancelAppointment(Appointment appointment)
        {
            if (canCancleAppointment(appointment))
            {
                appointment.Patient = await _unitOfWork.PatientRepository.GetByIdAsync(appointment.PatientId);
                await _emailService.SendCancelAppointmentEmail(appointment);
                await _unitOfWork.GetRepository<AppointmentRepository>().DeleteAsync(appointment);
                await _unitOfWork.CompleteAsync();
                return true;
            }

            return false;
        }
        
        public async Task<List<Appointment>> GetDoctorAppointments(Guid id)
        {
            var appointments = await _unitOfWork.GetRepository<AppointmentRepository>().GetAllAppointmentsForDoctor(id);
            //var doctorAppointments = appointments.Where(x => x.DoctorId == id);
            return appointments.ToList();
        }
        
        public async Task<List<Appointment>> GetPatientAppointments(Guid id)
        {
            var appointments = await _unitOfWork.GetRepository<AppointmentRepository>().GetAllAppointmentsForPatient(id);
            //var doctorAppointments = appointments.Where(x => x.DoctorId == id);
            return appointments.ToList();
        }
        
        
        private bool canCancleAppointment(Appointment appointment)
        {
            if(DateTime.Now.AddDays(1).CompareTo(appointment.Duration.From) < 0)
                return true;
            return false;
        }
      
    }
}