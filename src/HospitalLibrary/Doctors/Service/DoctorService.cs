using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Doctors.Service
{
    public class DoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Doctor>> GetAll()
        {
            return await _unitOfWork.DoctorRepository.GetAllDoctors();
        }
        
        public async Task<List<Doctor>> GetAllGeneral()
        {
            return await _unitOfWork.DoctorRepository.GetAllDoctorsBySpecialization();
        }

        public async Task<IEnumerable<DateRange>> generateFreeTimeSpans(DateRange selectedDateSpan, Guid doctorId)
        {
            IEnumerable<DateRange> busyHours = await getBusyHours(selectedDateSpan, doctorId);
            IEnumerable<DateRange> freeHours = new List<DateRange>();
            DateTime endDate = selectedDateSpan.To;
            WorkingSchedule doctorsSchedule = await _unitOfWork.DoctorRepository.GetDoctorWorkingSchedule(doctorId);
            int startScheduleHour = doctorsSchedule.DayOfWork.From.Hour;
            int startScheduleMin = doctorsSchedule.DayOfWork.From.Minute;
            
            DateTime startDate = new DateTime(selectedDateSpan.From.Year,selectedDateSpan.From.Month,selectedDateSpan.From.Day,startScheduleHour,startScheduleMin,0);
            while (startDate< endDate)
            {
                DateTime newScheduleStart = startDate;

                for (int i = 0; i < 15; i++)
                {
                    DateRange newScheduleRange = new DateRange();
                    newScheduleRange.From = newScheduleStart;
                    newScheduleRange.To = newScheduleStart.AddMinutes(30);
                    if (checkHolidayAndAppointmentAvailability(busyHours, newScheduleRange))
                    {
                        freeHours = freeHours.Append(newScheduleRange);
                    }
                    newScheduleStart.AddMinutes(30);
                }

                startDate =startDate.AddDays(1);
            }

            return freeHours;

        }

        public bool checkHolidayAndAppointmentAvailability(IEnumerable<DateRange> busyHours, DateRange newSschedule)
        {
            foreach (var range in busyHours)
            {
                if (!CheckDocotrsAvailabilityByDate(range, newSschedule) ||
                    !CheckDocotrsAvailabilityByTime(range, newSschedule))
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
        public async Task<IEnumerable<DateRange>> getBusyHours(DateRange selectedDateSpan, Guid doctorId)
        {
            IEnumerable<Appointment> appointments = await GetDoctorsAppointmentsInTimeSpan(selectedDateSpan, doctorId);
            IEnumerable<Holiday> holidays = await GetDoctorsHolidaysInTimeSpan(selectedDateSpan, doctorId);
            IEnumerable<DateRange> busyHours = new List<DateRange>();
            foreach (var app in appointments)
            {
                busyHours.Append(app.Duration);
            }
            foreach (var app in holidays)
            {
                busyHours.Append(app.DateRange);
            }

            return busyHours;

        }

        public async Task<IEnumerable<Appointment>> GetDoctorsAppointmentsInTimeSpan(DateRange span,Guid doctorId)
        {
            IEnumerable<Appointment> allAppointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsForDoctor(doctorId);
            IEnumerable<Appointment> filteredAppoontments = FiltertimespanAppointments(allAppointments, span);
            
            return filteredAppoontments;
        }
        
        public async Task<IEnumerable<Holiday>> GetDoctorsHolidaysInTimeSpan(DateRange span,Guid doctorId)
        {
            IEnumerable<DateRange> holidayDates = new List<DateRange>();
            IEnumerable<Holiday> allHolidays = await _unitOfWork.HolidayRepository.GetAllHolidaysForDoctor(doctorId);
            IEnumerable<Holiday> filteredHolidays = FiltertimespanHolidays(allHolidays, span);
            foreach (var app in filteredHolidays)
            {
                holidayDates.Append(app.DateRange);
            }
            return filteredHolidays;
        }

        public IEnumerable<Holiday> FiltertimespanHolidays(IEnumerable<Holiday> allHolidays, DateRange span)
        {
            IEnumerable<Holiday> filteredHolidays = new List<Holiday>();
            foreach (var hol in allHolidays)
            {
                if (!CheckSpan(hol.DateRange, span))
                {
                    filteredHolidays.Append(hol);
                }
            }

            return filteredHolidays;
        }
        
        public IEnumerable<Appointment> FiltertimespanAppointments(IEnumerable<Appointment> allAppointments, DateRange span)
        {
            IEnumerable<Appointment> filteredAppontments = new List<Appointment>();
            foreach (var app in allAppointments)
            {
                if (!CheckSpan(app.Duration, span))
                {
                    filteredAppontments.Append(app);
                }
            }

            return filteredAppontments;
        }
        
        private bool CheckSpan(DateRange scheduled, DateRange newSchedule)
        {
            return newSchedule.From > scheduled.To || newSchedule.To < scheduled.From;
        }

        public async Task<List<Doctor>> GetAllGeneralWithRequirements()
        {
            List<Doctor> doctors = await GetAllGeneral();
            List<Doctor> availableDoctors = new List<Doctor>();
            int minimumPatients = await GetMinimumPatients();
            foreach (var d in doctors)
            {
                if (d.Patients.ToArray().Length <= minimumPatients + 2)
                {
                    availableDoctors.Add(d);
                }
            }
            return availableDoctors;
        }

        public async Task<int> GetMinimumPatients()
        {
            int minimum = 2000000;
            List<Doctor> doctors = await GetAllGeneral();
            foreach (var d in doctors)
            {
                if (d.Patients.ToArray().Length < minimum)
                {
                    minimum = d.Patients.ToArray().Length;
                }
            }
            return minimum;
        }

      

        public async Task<Doctor> CreateDoctor(Doctor doctor)
        {
            var password = PasswordHasher.HashPassword(doctor.Password);
            doctor.Password = password;
            doctor.UserRole = UserRole.Doctor;
            var newDoctor =await _unitOfWork.DoctorRepository.CreateAsync(doctor);
            
            await _unitOfWork.CompleteAsync();
            return newDoctor;
        }

        public async Task<Doctor> GetByUsername(string username)
        {
            var doc = await _unitOfWork.DoctorRepository.GetByUsername(username);
            if (doc == null)
            {
                throw new DoctorNotExist("Doctor not exist");
            }

            return doc;
        }

        public async Task<Doctor> GetById(Guid id)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return doctor;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(id);
            if (doctor == null) { return false; }
            await _unitOfWork.DoctorRepository.DeleteAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}