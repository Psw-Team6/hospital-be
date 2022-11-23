using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Patient>> GetAllHospitalizedPatientsAsync()
        {
            return await DbSet.Where(patient => patient.PatientAdmissions.Any())
                .Include(x => x.PatientAdmissions)
                .ToListAsync();
        }
    }
}