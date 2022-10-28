using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;

namespace HospitalLibrary.Appointments.Service
{
    public class ScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> ScheduleAppointment(Appointment appointment)
        {
            await DoctorNotExist(appointment);
            await PatientNotExist(appointment);
            //await CheckDoctorAvailability(appointment);
            var app = await _unitOfWork.AppointmentRepository.CreateAsync(appointment);
            await _unitOfWork.CompleteAsync();
            return app;
        }

        private async Task DoctorNotExist(Appointment appointment)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(appointment.DoctorId);
            if (doctor == null)
            {
                throw new DoctorNotExist("Doctor with id: " + appointment.DoctorId + "does not exist");
            }
        }
        private async Task PatientNotExist(Appointment appointment)
        {
            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(appointment.PatientId);
            if (patient == null)
            {
                throw new DoctorException("Patient with id: " + appointment.PatientId + "does not exist");
            }
        }

        // private async Task CheckDoctorAvailability(Appointment appointment)
        // {
        //     var isAvailable = await IsDoctorAvailable(appointment);
        //     if (!isAvailable)
        //     {
        //         throw new DoctorIsNotAvailable("Doctor is not available ");
        //     }
        // }

        // private async Task<bool> IsDoctorAvailable(Appointment appointment)
        // {
        //     var doctorWorkingSchedule = await _unitOfWork.DoctorRepository.GetDoctorWorkingSchedule(appointment.DoctorId);
        //     //var doctorWorkingSchedule = doctor.WorkingSchedule;
        //     if (appointment.TimeSlot.StartTime.Date < doctorWorkingSchedule.StartUpDate ||
        //         appointment.TimeSlot.StartTime.Date > doctorWorkingSchedule.ExpiresDate)
        //         return false;
        //     var appointmentFinish = appointment.TimeSlot.StartTime.Add(appointment.TimeSlot.Duration);
        //     var doctorFinish = doctorWorkingSchedule.StartTime.Add(doctorWorkingSchedule.Duration);
        //     return appointment.TimeSlot.StartTime.TimeOfDay >= doctorWorkingSchedule.StartTime.TimeOfDay 
        //            && appointmentFinish.TimeOfDay <= doctorFinish.TimeOfDay;
        // }
    }
}