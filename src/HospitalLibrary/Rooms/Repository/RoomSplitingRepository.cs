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
    public class RoomSplitingRepository : GenericRepository<RoomSpliting>, IRoomSplitingRepository
    {
        public RoomSplitingRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<List<RoomSpliting>> GetAllSplittingByRoomId(Guid roomId)
        {
            return await  DbSet.Where(roomSpliting => roomSpliting.Id == roomId)
                .ToListAsync();
        }
        
    }
}