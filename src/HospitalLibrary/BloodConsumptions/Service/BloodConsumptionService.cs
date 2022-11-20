using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodUnits.Model;
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


        public async Task<List<BloodConsumption>> CreateConsumptions(BloodConsumptionCreateDto dto,Guid doctorId)
        {
            if (_unitOfWork.BloodUnitRepository.GetUnitsAmountByType(dto.BloodType).Result < dto.Amount)
                return null;

            var results = new List<BloodConsumption>();
            foreach (BloodUnit unit in BloodUnitsForConsumptions(dto).Result)
                results.Add(CreateConsumption(unit,dto).Result);
            
            return await Task.Run(() =>results);

        }
        
        private async Task<IEnumerable<BloodUnit>> BloodUnitsForConsumptions(BloodConsumptionCreateDto dto)
        {
            var result = new List<BloodUnit>();
            var units = _unitOfWork.BloodUnitRepository.GetSortUnitsByType(dto.BloodType).Result.ToList();
            while (AmountSumOfUnits(result) < dto.Amount)
            {
                result.Add(units[0]);
                units.RemoveAt(0);
            }
            return await Task.Run(() =>result);
        }

        private int AmountSumOfUnits(List<BloodUnit> units)
        {
            var sum = 0;
            if (units != null)
                foreach (BloodUnit unit in units)
                    sum += unit.Amount;
            
            return sum;
        }

        private async Task<BloodConsumption> CreateConsumption(BloodUnit unit, BloodConsumptionCreateDto dto)
        {
            BloodConsumption consumption = new BloodConsumption();
            consumption.Date = DateTime.Now;
            consumption.DoctorId = dto.doctorId;
            consumption.Purpose = dto.Purpose;
            consumption.BloodUnit = unit;
            consumption.BloodUnitId = unit.Id;
            if (unit.Amount <= dto.Amount)
            {
                dto.Amount -= unit.Amount;
                consumption.Amount = unit.Amount;
                unit.decreseAmount(unit.Amount);
            }
            else
            {
                consumption.Amount = dto.Amount;
                unit.decreseAmount(dto.Amount);
            }
            var result =await _unitOfWork.BloodConsumptionRepository.CreateAsync(consumption);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        
        
    }
}