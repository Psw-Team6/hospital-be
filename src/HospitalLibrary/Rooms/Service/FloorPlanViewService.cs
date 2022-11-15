using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
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