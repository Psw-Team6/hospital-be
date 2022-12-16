using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Doctors.Repository
{
    public interface IDoctorRepository: IGenericRepository<Doctor>
    {
        Task<List<Doctor>> GetAllDoctors();
        Task<WorkingSchedule> GetDoctorWorkingSchedule(Guid doctorId);
        Task<Doctor> GetByUsername(string username);
        Task<List<Doctor>> GetAllDoctorsBySpecialization(); 
        Task<Doctor> GetAllDoctorsBySIdAsync(Guid id);
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialization(Guid specializationId);
        Task<List<Doctor>> GetBySpecificSpecialisation(String specialization);
        Task<Doctor> GetDoctorSpecialization(Guid id);

    }
}