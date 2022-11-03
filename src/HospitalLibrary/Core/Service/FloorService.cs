using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service
{
    public class FloorService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public FloorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<Floor>> GetAll()
        {
            return await _unitOfWork.FloorRepository.GetAllFloors();
        }
        
    }
}