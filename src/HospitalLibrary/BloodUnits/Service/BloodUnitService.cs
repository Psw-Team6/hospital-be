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
        private readonly IBloodUnitRepository _bloodUnitRepository;

        public BloodUnitService(IUnitOfWork unitOfWork, IBloodUnitRepository bloodUnitRepository)
        {
            _unitOfWork = unitOfWork;
            _bloodUnitRepository = bloodUnitRepository;
        }

        public async Task<IEnumerable<BloodUnitDto>> GetUnitsGroupByType()
        {
            return await _unitOfWork.GetRepository<BloodUnitRepository>().GetUnitsGroupByType();
        }

        public async Task<bool> Update(BloodUnit bu)  
        {
            await _unitOfWork.GetRepository<BloodUnitRepository>().UpdateAsync(bu);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<BloodUnit> Create(BloodUnit bu)
        {
            BloodUnit bloodUnit = await _unitOfWork.GetRepository<BloodUnitRepository>().CreateAsync(bu);
            await _unitOfWork.CompleteAsync();
            return bloodUnit;
        }
    }
}