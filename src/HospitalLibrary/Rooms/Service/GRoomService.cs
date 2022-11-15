using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public class GRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GRoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GRoom>> GetAllGRooms()
        {
           return await _unitOfWork.GRoomRepository.GetAllGRooms();
        }
    }
}