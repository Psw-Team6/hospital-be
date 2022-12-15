using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Rooms.Repository
{
    public class RoomMerginRepository:GenericRepository<RoomMerging>, IRoomMergingRepository
    {
        public RoomMerginRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<List<RoomMerging>> GetAllMergingByRoomId(Guid roomId)
        {
            return await DbSet.Where(equipmentMerge => equipmentMerge.Room1Id == roomId || equipmentMerge.Room2Id == roomId )
                .ToListAsync();
        }
        
    }
}