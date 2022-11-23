using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Patients.Repository
{
    public class PatientRepository:GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            return await DbSet.Include(p => p.Address)
                .Include(p => p.Feedbacks)
                .ToListAsync();
        }

        public async Task<Patient> GetPatientById(Guid id)
        {
            return await DbSet.Include(p => p.Address)
                .Include(p => p.Feedbacks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}