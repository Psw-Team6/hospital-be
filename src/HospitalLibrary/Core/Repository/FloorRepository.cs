using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class FloorRepository :GenericRepository<Floor>, IFloorRepository
    {
        public FloorRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        public async Task<List<Floor>> GetAllFloors()
        {
            return await  DbSet.Include(d => d.Id)
                .Include(d => d.Name)
                .Include(d => d.BuildingId)
                .Include(d => d.FloorNumber)
                .ToListAsync();
        }
        
    }
}