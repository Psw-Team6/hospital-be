using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Prescriptions.Repository
{
    public class BloodPrescriptionRepository : GenericRepository<BloodPrescription>, IBloodPrescriptionRepository
    {
        public BloodPrescriptionRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        
        }
        
        public async Task<BloodPrescription> GetBloodById(Guid id)
        {
            return await DbSet
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}