using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public class RoomEventsService : IRoomEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomEventsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoomEvent>> GetAll()
        {
            return await _unitOfWork.RoomEventRepository.GetAllAsync();
        }
    }
}