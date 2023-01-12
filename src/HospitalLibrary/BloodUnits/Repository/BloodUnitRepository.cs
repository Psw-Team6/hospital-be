using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public async Task<IEnumerable<BloodUnitDto>> GetUnitsGroupByType()
        {
            return await DbSet.GroupBy(x => x.BloodType)
                .Select(g => new BloodUnitDto(g.Key, g.Select(s => s.Amount).Sum()))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<BloodUnit>> GetSortUnitsByType(BloodType bloodType)
        {
            return await DbSet.Where(x => x.BloodType == bloodType && x.Amount>0).OrderByDescending(z=>z.Amount).ToListAsync();
        }
        
        public async Task<int> GetUnitsAmountByType(BloodType bloodType)
        {
            return await Task.FromResult(DbSet.Where(x => x.BloodType == bloodType).Sum(i => i.Amount));
        }

        public async Task<IEnumerable<BloodUnit>> GetUrgentUnits()
        {
            return await DbSet.Where(x => x.Source == "URGENT").ToListAsync();
        }

    }
}