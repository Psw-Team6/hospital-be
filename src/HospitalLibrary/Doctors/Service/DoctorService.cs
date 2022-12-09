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


        public async Task<IEnumerable<Appointment>> GetDoctorsAppointmentsInTimeSpan(DateRange span,Guid doctorId)
        {
            IEnumerable<Appointment> allAppointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsForDoctor(doctorId);
            IEnumerable<Appointment> filteredAppoontments = FiltertimespanAppointments(allAppointments, span);
            return filteredAppoontments;
        }
        
        public async Task<IEnumerable<Holiday>> GetDoctorsHolidaysInTimeSpan(DateRange span,Guid doctorId)
        {
            IEnumerable<Holiday> allHolidays = await _unitOfWork.HolidayRepository.GetAllHolidaysForDoctor(doctorId);
            IEnumerable<Holiday> filteredHolidays = FiltertimespanHolidays(allHolidays, span);
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