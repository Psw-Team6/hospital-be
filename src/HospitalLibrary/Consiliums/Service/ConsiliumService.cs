using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Consiliums.Model;

namespace HospitalLibrary.Consiliums.Service
{
    public class ConsiliumService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsiliumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Consilium>> GetAll()
        {
            return await _unitOfWork.ConsiliumRepository.GetAllAsync();
        }

        public async Task<Consilium> ScheduleConsilium(Consilium consilium)
        {
            var newConsilium = await _unitOfWork.ConsiliumRepository.CreateAsync(consilium);
            return newConsilium;
        }
    }
}