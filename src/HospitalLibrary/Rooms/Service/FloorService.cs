using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
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
        
        public async Task<bool> Update(Floor floor)
        {
            await _unitOfWork.FloorRepository.UpdateAsync(floor);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}