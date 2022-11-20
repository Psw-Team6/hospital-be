using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service
{
    public class BuildingService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public BuildingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<Building>> GetAll()
        {
            return await _unitOfWork.BuildingRepository.GetAllBuildings();
        }
    }
}