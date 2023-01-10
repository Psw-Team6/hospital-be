using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Common;

namespace HospitalLibrary.BloodUnits.Repository
{
    public interface IBloodUnitRepository: IGenericRepository<BloodUnit>
    {
        Task<IEnumerable<BloodUnitDto>> GetUnitsGroupByType();
        Task<int> GetUnitsAmountByType(BloodType bloodType);
        Task<IEnumerable<BloodUnit>> GetSortUnitsByType(BloodType bloodType);

    }
}