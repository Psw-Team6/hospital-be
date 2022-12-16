using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Repository
{
    public interface IRoomSplitingRepository:IGenericRepository<RoomSpliting>
    {
        Task<List<RoomSpliting>> GetAllSplittingByRoomId(Guid roomId);
    }
}