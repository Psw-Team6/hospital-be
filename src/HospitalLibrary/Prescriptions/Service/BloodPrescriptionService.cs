using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Prescriptions.Model;

namespace HospitalLibrary.Prescriptions.Service
{
    public class BloodPrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BloodPrescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
        public async Task<List<BloodPrescription>> Create(BloodPrescription bloodPrescription)
        {
            if (_unitOfWork.BloodUnitRepository.GetUnitsAmountByType(bloodPrescription.BloodType).Result < bloodPrescription.Amount)
                return null;

            var results = new List<BloodPrescription>();
            foreach (BloodUnit unit in BloodUnitsForConsumptions(bloodPrescription).Result)
                results.Add(CreateConsumption(unit,bloodPrescription).Result);
            
            return await Task.Run(() =>results);

        }
        
        private async Task<IEnumerable<BloodUnit>> BloodUnitsForConsumptions(BloodPrescription dto)
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
        
        private async Task<BloodPrescription> CreateConsumption(BloodUnit unit, BloodPrescription dto)
        {
            BloodPrescription consumption = new BloodPrescription();
            consumption.BloodType = dto.BloodType;
            consumption.Amount = dto.Amount;
            consumption.Description = "Treatment";
            consumption.TreatmentReportId = dto.TreatmentReportId;
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
            var result =await _unitOfWork.BloodPrescriptionRepository.CreateAsync(consumption);
            await _unitOfWork.CompleteAsync();
            return result;
        }
        
        public async Task<BloodPrescription> GetBloodById(Guid id)
        {
            var prescription = await _unitOfWork.BloodPrescriptionRepository.GetBloodById(id);
            await _unitOfWork.CompleteAsync();
            return prescription;
        }
        
        
    }
}