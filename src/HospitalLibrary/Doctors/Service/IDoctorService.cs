using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Doctors.Service
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAll();
        Task<IEnumerable<DateRange>> generateFreeTimeSpans(DateRange selectedDateSpan, Guid doctorId);
        Task<IEnumerable<Appointment>> GetDoctorsAppointmentsInTimeSpan(DateRange span, Guid doctorId);
        Task<IEnumerable<Holiday>> GetDoctorsHolidaysInTimeSpan(DateRange span, Guid doctorId);
        Task<List<Doctor>> GetAllGeneralWithRequirements();
        Task<Doctor> CreateDoctor(Doctor doctor);
        Task<List<Doctor>> GetDoctorsForConsilium(Consilium consilium);
        Task<Doctor> GetByUsername(string username);
        Task<List<Doctor>> GetBySpecialisation(string specialisation);
        Task<Doctor> GetById(Guid id);
        Task<bool> DeleteById(Guid id);
        Task<List<Doctor>> GetDoctorsBySpecializations(IEnumerable<Specialization> specializations);
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialization(Guid specId);
        Task<Doctor> GetDoctorSpecialization(Guid id);

        Task<IEnumerable<AppointmentSuggestion>> GetFreeTermsByDoctorPriority(
            AppointmentSuggestion appointmentSuggestion);
    }
}