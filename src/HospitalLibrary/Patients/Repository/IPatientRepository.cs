
 using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Patients.Repository
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetAllHospitalizedPatientsAsync();
        Task<List<Patient>> GetAllPatients();
        Task<Patient> GetPatientById(Guid id);
    }
}