using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repository
{
    public class FloorRepository :GenericRepository<Floor>, IFloorRepository
    {
        public FloorRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        public async Task<List<Floor>> GetAllFloors()
        {
            return await  DbSet
                .ToListAsync();
        }
        
    }
}