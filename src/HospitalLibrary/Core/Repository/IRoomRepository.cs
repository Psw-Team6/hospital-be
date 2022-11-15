using System;
using HospitalLibrary.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;

namespace HospitalLibrary.Core.Repository
{
    public interface IRoomRepository:IGenericRepository<Room>
    {
        Task<List<Room>> GetAllRooms();

        Task<List<Room>> GetAllRoomsByBuildingIdAndFloorId(Guid buildingId, Guid floorId);
    }
}
