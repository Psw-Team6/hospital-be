using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.Common;

namespace HospitalLibrary.BloodConsumptions.Repository
{
    public interface IBloodConsumptionRepository : IGenericRepository<BloodConsumption>
    {
        Task<IEnumerable<BloodConsumption>> GetByBloodBankName(String bloodBankName);
        Task<IEnumerable<BloodConsumption>> GetByDoctorId(Guid doctorId);
        Task<IEnumerable<BloodConsumption>> GetAllConsumptions();
    }
}