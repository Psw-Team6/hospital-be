using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Common;

namespace HospitalLibrary.BloodUnits.Repository
{
    public interface IBloodUnitRepository: IGenericRepository<BloodUnit>
    {
        Task<IEnumerable<BloodUnitDto>> GetUnits();
        Task<BloodUnit> GetUnitAvailableUnitByType(BloodType bloodType, int amount);
    }
}