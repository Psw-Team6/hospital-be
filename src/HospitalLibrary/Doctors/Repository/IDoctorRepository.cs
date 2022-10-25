using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Doctors.Repository
{
    public interface IDoctorRepository: IGenericRepository<Doctor>
    {
        Task<List<Doctor>> GetAllDoctors();
    }
}