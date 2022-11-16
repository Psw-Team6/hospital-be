using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.Common;

namespace HospitalLibrary.BloodConsumptions.Service
{
    public class BloodConsumptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BloodConsumptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BloodConsumption>> GetAllConsumptions()
        {
            return await _unitOfWork.BloodConsumptionRepository.GetAllConsumptions();
        }
        
        public async Task<IEnumerable<BloodConsumption>> GetByBloodBankName(string bloodBankName)
        {
            return await _unitOfWork.BloodConsumptionRepository.GetByBloodBankName(bloodBankName);
        }
        
        public async Task<IEnumerable<BloodConsumption>> GetByDoctorId(Guid docotorId)
        {
            return await _unitOfWork.BloodConsumptionRepository.GetByDoctorId(docotorId);
        }


        public async Task<BloodConsumption> CreateConsumption(BloodConsumption consumption)
        {
            consumption.Date = DateTime.Now;
            consumption.BloodUnit = _unitOfWork.BloodUnitRepository.GetByIdAsync(consumption.BloodUnitId).Result;
            consumption.BloodUnit.decreseAmount(consumption.Amount);
            var result =await _unitOfWork.BloodConsumptionRepository.CreateAsync(consumption);
            await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}