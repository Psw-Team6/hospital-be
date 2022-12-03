using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;

namespace HospitalLibrary.SharedModel.Service
{
    public class AllergenService
    {

        private readonly IUnitOfWork _unitOfWork;

        public AllergenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Allergen>> GetAll()
        {
            return await _unitOfWork.AllergenRepository.GetAllAsync();
        }

    }
}