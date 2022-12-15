using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAll();
        Task<IEnumerable<Room>> GetAllByBuildingIdAndFloorId(Guid buildingId, Guid floorId);
        Task<Room> GetById(Guid id);
        Task<bool> Update(Room room);}
}