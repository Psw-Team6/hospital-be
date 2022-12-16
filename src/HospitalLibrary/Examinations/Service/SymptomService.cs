using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;

namespace HospitalLibrary.Examinations.Service
{
    public class SymptomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SymptomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Symptom>> GetAllSymptoms()
        {
            return await _unitOfWork.SymptomRepository.GetAllAsync() as List<Symptom>;
        }
    }
}