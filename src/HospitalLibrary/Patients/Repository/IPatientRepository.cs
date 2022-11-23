using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Patients.Repository
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetAllHospitalizedPatientsAsync();
    }
}