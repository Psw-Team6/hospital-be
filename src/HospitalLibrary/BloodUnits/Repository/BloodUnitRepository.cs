using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.BloodUnits.Repository
{
    public class BloodUnitRepository : GenericRepository<BloodUnit>, IBloodUnitRepository
    {
        public BloodUnitRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            
        }
        
        public async Task<IEnumerable<BloodUnitDto>> GetUnits()
        {
            return DbSet.GroupBy(x => x.BloodType)
                .Select(g => new BloodUnitDto(g.Key, g.Select(s => s.Amount).Sum()))
                .AsEnumerable<BloodUnitDto>();;
        }

        public async Task<BloodUnit> GetUnitAvailableUnitByType(BloodType bloodType, int amount)
        {
            return await DbSet.Where(x => x.BloodType==bloodType && x.Amount>= amount)
                        .FirstOrDefaultAsync();
        }

    }
}