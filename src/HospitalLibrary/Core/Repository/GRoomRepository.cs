using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class GRoomRepository:GenericRepository<GRoom>,IGRoomRepository
    {
        public GRoomRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GRoom>> GetAllGRooms()
        {
            return await DbSet.ToListAsync();
        }
    }
}