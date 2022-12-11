using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Consiliums.Repository
{
    public class ConsiliumRepository : GenericRepository<Consilium> , IConsiliumRepository
    {
        public ConsiliumRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Consilium>> GetConsiliumsForDoctor()
        {
            return await DbSet.Include(c => c.Doctors)
                .ToListAsync();
        }
    }
}