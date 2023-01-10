using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Repository
{
    public interface IRoomMergingRepository:IGenericRepository<RoomMerging>
    {
        Task<List<RoomMerging>> GetAllMergingByRoomId(Guid originalRoomId);
    }
}