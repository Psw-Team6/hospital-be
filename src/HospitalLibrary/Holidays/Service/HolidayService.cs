using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                throw new DoctorIsNotAvailable("You have already scheduled appointment in that period.");
            }
            
        }

        private async Task<bool> CheckDocorScheduledAppointments(Holiday holiday)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsForDoctor(holiday.DoctorId);
            foreach (var appointment in appointments)
            {
                if (!CheckDocotrsAvailabilityByDate(appointment.Duration, holiday))
                {
                    return false;
                }
            }

            return true;
        }
        
        private async Task<bool> CheckDocorScheduledHolidays(Holiday holiday)
        {
            var holidays = await _unitOfWork.HolidayRepository.GetAllHolidaysForDoctor(holiday.DoctorId);
            foreach (var h in holidays)
            {
                if (!CheckDocotrsAvailabilityByDate(h.DateRange, holiday))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckDocotrsAvailabilityByDate(DateRange scheduled, Holiday holiday)
        {
            return holiday.DateRange.From > scheduled.To.Date || holiday.DateRange.To < scheduled.From.Date;
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
        
       

  