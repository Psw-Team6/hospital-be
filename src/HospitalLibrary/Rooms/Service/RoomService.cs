using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Rooms.Service
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IAppointmentService _appointmentService;
        public RoomService(IUnitOfWork unitOfWork, IAppointmentService appointmentService)
        {
            _unitOfWork = unitOfWork;
            _appointmentService = appointmentService;
        }

        public RoomService(IUnitOfWork unitOfWork)
        {
            throw new NotImplementedException();
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

        public async Task<Room> MergeRooms(Room room1, Room room2)
        {
            Room newRoom = new Room();
            newRoom = room1;
            //TREBACE OVO VRV foreach (var room2Bed in room2.Beds) newRoom.Beds.Add(room2Bed);
            newRoom.Id = Guid.NewGuid();
            GRoom newGroom = new GRoom();
            
            GRoom groom1 = await _unitOfWork.GRoomRepository.GetByIdAsync(room1.GRoomId);
            GRoom groom2 = await _unitOfWork.GRoomRepository.GetByIdAsync(room2.GRoomId);
            
            newGroom.RoomId = newRoom.Id;
            newGroom.Id = Guid.NewGuid();
            newGroom.PositionX = Math.Min(groom1.PositionX, groom2.PositionX);
            newGroom.PositionY = Math.Min(groom1.PositionY, groom2.PositionY);
            newGroom.Lenght = Math.Max(groom1.PositionX + groom1.Lenght, groom2.PositionX + groom2.Lenght) - newGroom.PositionX;
            newGroom.Width = Math.Max(groom1.PositionY + groom1.Width, groom2.PositionY + groom2.Width) - newGroom.PositionY;

            await _unitOfWork.GRoomRepository.CreateAsync(newGroom);
            await _unitOfWork.RoomRepository.CreateAsync(newRoom);
            await _unitOfWork.GRoomRepository.DeleteAsync(groom1);
            await _unitOfWork.GRoomRepository.DeleteAsync(groom2);
            await _unitOfWork.RoomRepository.DeleteAsync(room1);
            await _unitOfWork.RoomRepository.DeleteAsync(room2);
            await _unitOfWork.CompleteAsync();

            return newRoom;
        }
        
        public async Task<Room> SplitRoom(Room room1)
        {
            Room newRoom1 = new Room();
            newRoom1 = room1;
            newRoom1.Id = Guid.NewGuid();
            
            Room newRoom2 = new Room();
            newRoom2.Id = Guid.NewGuid();
            
            GRoom originalGroom = await _unitOfWork.GRoomRepository.GetByIdAsync(room1.GRoomId);
            
            GRoom newGroom1 = new GRoom();
            newGroom1.RoomId = newRoom1.Id;
            newGroom1.Id = Guid.NewGuid();
            newGroom1.PositionX = originalGroom.PositionX;
            newGroom1.PositionY = originalGroom.PositionY;
            newGroom1.Lenght = originalGroom.Lenght / 2;
            newGroom1.Width = originalGroom.Width / 2;
            
            GRoom newGroom2 = new GRoom();
            newGroom2.RoomId = newRoom2.Id;
            newGroom2.Id = Guid.NewGuid();
            newGroom2.Lenght = originalGroom.Lenght - newGroom1.Lenght;
            newGroom2.Width = originalGroom.Width - newGroom1.Width;
            newGroom2.PositionX = newGroom1.PositionX + newGroom1.Lenght;
            newGroom2.PositionY = newGroom1.PositionY + newGroom1.Width;

            await _unitOfWork.GRoomRepository.CreateAsync(newGroom1);
            await _unitOfWork.GRoomRepository.CreateAsync(newGroom2);
            
            await _unitOfWork.RoomRepository.CreateAsync(newRoom1);
            await _unitOfWork.RoomRepository.CreateAsync(newRoom2);
            
            await _unitOfWork.RoomRepository.DeleteAsync(room1);
            await _unitOfWork.GRoomRepository.DeleteAsync(originalGroom);
            
            await _unitOfWork.CompleteAsync();

            return newRoom1;
        }
        
    }
}
