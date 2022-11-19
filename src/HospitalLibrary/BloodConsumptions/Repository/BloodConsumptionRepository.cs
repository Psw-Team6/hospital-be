using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.BloodConsumptions.Repository
{
    public class BloodConsumptionRepository : GenericRepository<BloodConsumption>, IBloodConsumptionRepository
    {
        public BloodConsumptionRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<IEnumerable<BloodConsumption>> GetAllConsumptions()
        {
            return await DbSet.Include(x => x.BloodUnit)
                .ToListAsync();
        }
        public async Task<IEnumerable<BloodConsumption>> GetByBloodBankName(String bloodBankName)
        {
            return await DbSet.Where(x => x.BloodUnit.BloodBankName==bloodBankName )
                .Include(x => x.BloodUnit)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<BloodConsumption>> GetByDoctorId(Guid doctorId)
        {
            return await DbSet.Where(x => x.DoctorId==doctorId )
                .Include(x => x.BloodUnit)
                .ToListAsync();
        }
        
    }
}