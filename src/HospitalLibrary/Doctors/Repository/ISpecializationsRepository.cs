using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Doctors.Repository
{
    public interface ISpecializationsRepository:IGenericRepository<Specialization>
    {
        Task<Specialization> GetBySpecializationName(String name);
    }
}