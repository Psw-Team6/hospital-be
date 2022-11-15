using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repository
{
    public class GRoomRepository:GenericRepository<GRoom>,IGRoomRepository
    {
        public GRoomRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GRoom>> GetAllGRooms()
        {
            return await DbSet
                .Include(x => x.Room)
                .Include(x => x.Room.Floor)
                .Include(x => x.Room.Floor.Building).ToListAsync();
        }
    }
}