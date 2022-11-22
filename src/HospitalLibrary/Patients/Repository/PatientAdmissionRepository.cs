using System;
using System.Linq;
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

        public async Task<PatientAdmission> GetPatientAdmissionByIdAsync(Guid id)
        {
            return await DbSet.Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}