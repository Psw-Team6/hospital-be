using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.BloodUnits.Repository;
using HospitalLibrary.Common;

namespace HospitalLibrary.BloodUnits.Service
{
    public class BloodUnitService
    {
        private readonly IUnitOfWork _unitOfWork;


        public BloodUnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BloodUnitDto>> GetUnitsGroupByType()
        {
            return await _unitOfWork.GetRepository<BloodUnitRepository>().GetUnitsGroupByType();
        }
        
    }
}