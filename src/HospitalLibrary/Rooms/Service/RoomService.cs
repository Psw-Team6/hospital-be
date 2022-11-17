using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public class RoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _unitOfWork.RoomRepository.GetAllRooms();
        }
        
        public async Task<IEnumerable<Room>> GetAllByBuildingIdAndFloorId(Guid buildingId, Guid floorId)
        {
            return await _unitOfWork.RoomRepository.GetAllRoomsByBuildingIdAndFloorId(buildingId, floorId);
        }

        public async Task<Room> GetById(Guid id)
        {
            return await _unitOfWork.RoomRepository.GetByIdAsync(id);
        }

        public async Task<bool> Update(Room room)
        {
            await _unitOfWork.RoomRepository.UpdateAsync(room);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
