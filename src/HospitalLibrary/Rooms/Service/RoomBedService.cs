using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public class RoomBedService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomBedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoomBed>> GetAll()
        {
            return await _unitOfWork.RoomBedRepository.GetAllAsync();
        }
        
    }
}