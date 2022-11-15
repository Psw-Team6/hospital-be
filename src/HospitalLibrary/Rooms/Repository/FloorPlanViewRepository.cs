using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repository
{
    public class FloorPlanViewRepository:GenericRepository<FloorPlanView>, IFloorPlanViewRepository
    {
        public FloorPlanViewRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        public async Task<List<FloorPlanView>> GetAllFloorPlanViews()
        {
            return await  DbSet
                .ToListAsync();
        }
        
    }
}