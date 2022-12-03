using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Medicines.Repository
{
    public class MedicineRepository :GenericRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            
        }
        
        public async Task<List<Medicine>> GetAllMedicine()
        {
            return await DbSet
                .ToListAsync();
        }
        
        public async Task<int> GetAmountById(Guid id)
        {
            return await Task.FromResult(DbSet.Where(x => x.Id == id).Sum(i => i.Amount));
        }
    }
}