using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Holidays.Repository;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Holidays.Service
{
    public class HolidayService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HolidayService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Holiday>> GetAllHolidays()
        {
            var holidays = await _unitOfWork.GetRepository<HolidayRepository>().GetAllAsync();
            return holidays;
        }
        
        public async Task<Holiday> GetById(Guid id)
        {
            return await _unitOfWork.GetRepository<HolidayRepository>().GetByIdAsync(id);
        }

        public async Task<Holiday> ScheduleHoliday(Holiday holiday)
        {
            await ValidateHoliday(holiday);
            var newHoliday = await _unitOfWork.HolidayRepository.CreateAsync(holiday);
            await _unitOfWork.CompleteAsync();
            return newHoliday;
        }

        private async Task ValidateHoliday(Holiday holiday)
        {
            await DoctorNotExist(holiday);
            await CheckDoctorsSchedule(holiday);
            CheckDateRange(holiday);
        }

        private async Task DoctorNotExist(Holiday holiday)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(holiday.DoctorId);
            if (doctor == null)
            {
                throw new DoctorNotExist("Doctor does not exist.");
            }
        }
        
        private async Task CheckDoctorsSchedule(Holiday holiday)
        {
            
            if (!await  CheckDocorScheduledHolidays(holiday))
            {
                throw new DoctorIsNotAvailable("You already scheduled a holiday in that period.");
            }
            if (!await  CheckDocorScheduledAppointments(holiday))
            {
                if (holiday.IsUrgent)
                {
                    throw new DoctorIsNotAvailable("No replacement found for appointments scheduled in that period.");
                }
                throw new DoctorIsNotAvailable("You have already scheduled appointment in that period.");
            }
            
        }
        

        private async Task<bool> CheckReplacementsHolidays(Appointment appointment, Guid doctorId)
        {
            var holidays = await _unitOfWork.HolidayRepository.GetAllHolidaysForDoctor(doctorId);
            foreach (var holiday in holidays)
            {
                if (!CheckDocotrsAvailabilityByDate(holiday.DateRange, appointment.Duration))
                {
                    return false;
                }
            }

            return true;
        }

        

        private async Task<bool> CheckDocorScheduledAppointments(Holiday holiday)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsForDoctor(holiday.DoctorId);
            foreach (var appointment in appointments)
            {
                if (!CheckDocotrsAvailabilityByDate(appointment.Duration, holiday.DateRange))
                {
                    if (holiday.IsUrgent)
                    {
                        if (!await FindDoctorsReplacement(appointment, holiday.DoctorId))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        private async Task<bool> FindDoctorsReplacement(Appointment appointment,Guid doctorId)
        {
            var doctors = await _unitOfWork.DoctorRepository.GetAllDoctors();
            foreach (var dr in doctors)
            {
                if (doctorId != dr.Id)
                {
                    if (await CheckReplacementsSchedule(appointment, dr.Id))
                    {
                        appointment.Doctor = dr;
                        appointment.DoctorId = dr.Id;
                        await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
                        return true;
                    }
                }
                
            }
            return false;
        }
        
        private async Task<bool> CheckReplacementsSchedule(Appointment appointment, Guid doctorId)
        {
            var docotrIsFreeAppointments = await CheckReplacementsAppointments(appointment,doctorId);

            var docotrIsFreeHoliday = await CheckReplacementsHolidays(appointment,doctorId);
            return docotrIsFreeAppointments && docotrIsFreeHoliday;

        }
        
        private async Task<bool> CheckReplacementsAppointments(Appointment appointment, Guid doctorId)
        {
            Console.WriteLine("sagagsfdgadfasd");
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsForDoctor(doctorId);
            foreach (var app in appointments)
            {
                Console.WriteLine(app);
                if (!await CheckReplacementAvailability(app.Duration, appointment))
                {
                    Console.WriteLine("usao");
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> CheckReplacementAvailability(DateRange scheduled,Appointment appointment)
        {
            var doctorWorkingSchedule = await _unitOfWork.DoctorRepository.GetDoctorWorkingSchedule(appointment.DoctorId);
            if (CheckWorkingHours(appointment, doctorWorkingSchedule))
            {
                if (CheckDocotrsAvailabilityByDate(scheduled, appointment.Duration))
                {
                    return true;
                }
                return CheckDocotrsAvailabilityByTime(scheduled, appointment.Duration);
            }

            return false;

        }
        
        
        private static bool CheckWorkingHours(Appointment appointment, WorkingSchedule doctorWorkingSchedule)
        {
            return appointment.Duration.From.TimeOfDay >= doctorWorkingSchedule.DayOfWork.From.TimeOfDay
                   && appointment.Duration.To.TimeOfDay <= doctorWorkingSchedule.DayOfWork.To.TimeOfDay;
        }

        
        private async Task<bool> CheckDocorScheduledHolidays(Holiday holiday)
        {
            var holidays = await _unitOfWork.HolidayRepository.GetAllHolidaysForDoctor(holiday.DoctorId);
            foreach (var h in holidays)
            {
                if (!CheckDocotrsAvailabilityByDate(h.DateRange, holiday.DateRange))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckDocotrsAvailabilityByDate(DateRange scheduled, DateRange newSchedule)
        {
            return newSchedule.From.Date > scheduled.To.Date || newSchedule.To.Date < scheduled.From.Date;
        }
        private bool CheckDocotrsAvailabilityByTime(DateRange scheduled, DateRange newSchedule)
        {
            return newSchedule.From.Hour > scheduled.To.Hour || newSchedule.To.Hour < scheduled.From.Hour;
        }
        
        private static void CheckDateRange(Holiday holiday)
        {
            if (!holiday.DateRange.IsValidRange())
            {
                throw new DateRangeException("Date range is not valid");
            }

            if (holiday.DateRange.IsBeforeDate())
            {
                throw new DateRangeNotValid("Please select upcoming date");
            }

            if (!holiday.DateRange.CheckAfterMonthDate())
            {
                throw new DateRangeNotValid("Scheule your holiday at least one month earlier please.");
            }
        }
        
    }
}
        
       

  