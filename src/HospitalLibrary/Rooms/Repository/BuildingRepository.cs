using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repository
{
    public class BuildingRepository:GenericRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        public async Task<List<Building>> GetAllBuildings()
        {
            return await  DbSet
                .ToListAsync();
        }
    }
}