using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Prescriptions.Model;

namespace HospitalLibrary.Prescriptions.Service
{
    public class MedicinePrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicinePrescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<MedicinePrescription> Create(MedicinePrescription medicinePrescription)
        {
            if (_unitOfWork.MedicineRepository.GetAmountById(medicinePrescription.MedicineId).Result == 0)
            {
                return null;
            }

            var medicine = await _unitOfWork.MedicineRepository.GetByIdAsync(medicinePrescription.MedicineId);
            medicine.Amount-=1;
            await _unitOfWork.MedicineRepository.UpdateAsync(medicine);
            var newPrescription = await _unitOfWork.MedicinePrescriptionRepository.CreateAsync(medicinePrescription);
            await _unitOfWork.CompleteAsync();
            return newPrescription;
        }
        
        public async Task<MedicinePrescription> GetPrescriptionById(Guid id)
        {
            var prescription = await _unitOfWork.MedicinePrescriptionRepository.GetByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return prescription;
        }

        
    }
}