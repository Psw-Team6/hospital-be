using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service
{
    public class FloorPlanViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public FloorPlanViewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<FloorPlanView>> GetAll()
        {
            return await _unitOfWork.FloorPlanViewRepository.GetAllFloorPlanViews();
        }
        
    }
}