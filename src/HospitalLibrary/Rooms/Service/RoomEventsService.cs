using System;
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

        public async Task<RoomEvent> Create(RoomEvent roomEvent)
        {
            roomEvent.Id = Guid.NewGuid();
            roomEvent.TimeStamp = DateTime.Now;
            
            var result = await _unitOfWork.RoomEventRepository.CreateAsync(roomEvent);
            
            await _unitOfWork.CompleteAsync();
            
            return result;
        }
    }
}