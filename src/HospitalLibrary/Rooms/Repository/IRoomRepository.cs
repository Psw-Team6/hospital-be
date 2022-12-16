using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Repository
{
    public interface IRoomRepository:IGenericRepository<Room>
    {
        Task<List<Room>> GetAllRooms();

        Task<List<Room>> GetAllRoomsByBuildingIdAndFloorId(Guid buildingId, Guid floorId);
        Task<IEnumerable<Room>> GetAllMeetingRooms();
    }
}
