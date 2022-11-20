using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
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