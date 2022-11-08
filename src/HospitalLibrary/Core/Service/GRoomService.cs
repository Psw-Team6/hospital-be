using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service
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