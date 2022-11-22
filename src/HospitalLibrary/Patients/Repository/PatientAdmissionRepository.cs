using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Patients.Repository
{
    public class PatientAdmissionRepository:GenericRepository<PatientAdmission>, IPatientAdmissionRepository
    {
        public PatientAdmissionRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PatientAdmission>> GetAllPatientAdmissions()
        {
            return await DbSet.Include(p => p.Patient)
                .ToListAsync();
        }
    }
}