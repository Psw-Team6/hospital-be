using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Patients.Repository
{
    public class MaliciousPatientRepository:GenericRepository<MaliciousPatient>, IMaliciousPatientRepository
    {
        public MaliciousPatientRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<MaliciousPatient>> GetAllMaliciousPatients()
        {
            return await DbSet.Include(p => p.Patient)
                .Where(p => p.Malicious==true)
                .ToListAsync();
        }
        
        public async Task<MaliciousPatient> GetByPatientId(Guid patientId)
        {
            return await DbSet.Include(x => x.Patient)
                .Where(x => x.PatientId == patientId)
                .FirstOrDefaultAsync();
        }
    }
}