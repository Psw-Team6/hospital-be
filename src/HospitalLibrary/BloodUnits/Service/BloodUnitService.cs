using System.Collections.Generic;
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
        
        public async Task<IEnumerable<BloodUnitDto>> GetUnits()
        {
            var units =  await _unitOfWork.GetRepository<BloodUnitRepository>().GetUnits();
            return units;
        }
        
        public async Task<IEnumerable<BloodUnit>> GetAllBloodUnits()
        {
            return await _unitOfWork.GetRepository<BloodUnitRepository>().GetAllAsync();
        }

        public async Task<BloodUnit> GetAvailableBloodUnit(BloodType bloodType, int amount)
        {
          return await _unitOfWork.BloodUnitRepository.GetUnitAvailableUnitByType(bloodType, amount);
        }
        
    }
}