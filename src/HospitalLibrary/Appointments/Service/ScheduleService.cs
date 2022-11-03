using System;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;

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
            await ValidateAppointment(appointment);
            var app = await _unitOfWork.AppointmentRepository.CreateAsync(appointment);
            await _unitOfWork.CompleteAsync();
            return app;
        }

        private async Task ValidateAppointment(Appointment appointment)
        {
            CheckDateRange(appointment);
            await DoctorNotExist(appointment);
            await PatientNotExist(appointment);
            await CheckDoctorAvailability(appointment);
            await CheckPatientAvailability(appointment);
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

        private async Task CheckPatientAvailability(Appointment appointment)
        {
            var isPatientAvailable = await CheckPatientAvailabilityForAppointment(appointment);
            if (!isPatientAvailable)
            {
                throw new PatientException("Patient is not available.");
            }
        }

        private async Task CheckDoctorAvailability(Appointment appointment)
        {
            var isAvailableSchedule = await IsDoctorWorking(appointment);
            var isAvailableAppointment = await CheckDoctorAvailabilityForAppointment(appointment);
            if (!isAvailableSchedule)
            {
                throw new DoctorIsNotAvailable("You are  not available.Check your schedule.");
            }
            if (!isAvailableAppointment)
            {
                throw new DoctorIsNotAvailable("You have already scheduled appointment!");
            }
        }

        private async Task<bool> IsDoctorWorking(Appointment appointment)
        {
            var doctorWorkingSchedule = await _unitOfWork.DoctorRepository.GetDoctorWorkingSchedule(appointment.DoctorId);
            if (!CheckWorkingDate(appointment, doctorWorkingSchedule)) return false;
            return CheckWorkingHours(appointment, doctorWorkingSchedule);
        }

        private static bool CheckWorkingHours(Appointment appointment, WorkingSchedule doctorWorkingSchedule)
        {
            return appointment.Duration.From.TimeOfDay >= doctorWorkingSchedule.DayOfWork.From.TimeOfDay
                   && appointment.Duration.To.TimeOfDay <= doctorWorkingSchedule.DayOfWork.To.TimeOfDay;
        }

        private static bool CheckWorkingDate(Appointment appointment, WorkingSchedule doctorWorkingSchedule)
        {
            return appointment.Duration.From.Date >= doctorWorkingSchedule.ExpirationDate.From.Date 
                   && appointment.Duration.To.Date <= doctorWorkingSchedule.ExpirationDate.To.Date;
        }

        private async Task<bool> CheckDoctorAvailabilityForAppointment(Appointment appointment)
        {
           var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsForDoctor(appointment.DoctorId);
           return appointments.All(app => !app.IsDoctorConflicts(appointment));
        }
        private async Task<bool> CheckPatientAvailabilityForAppointment(Appointment appointment)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsForDoctor(appointment.DoctorId);
            return appointments.All(app => !appointment.IsPatientConflicts(app));
        }
        private static void CheckDateRange(Appointment appointment)
        {
            if (!appointment.Duration.IsValidRange())
            {
                throw new DateRangeException("Date range is not valid");
            }

            if (appointment.Duration.IsBeforeToday())
            {
                throw new DateRangeNotValid("Please select upcoming date");
            }
        }
    }
}