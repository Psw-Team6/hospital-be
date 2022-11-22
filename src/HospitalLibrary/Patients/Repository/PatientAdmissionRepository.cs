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

        public Task<PatientAdmission> GetPatientAdmissionByIdAsync(Guid id)
        {
            return  Task.FromResult(DbSet.Include(x => x.Patient)
                .FirstOrDefault(x => x.Id == id));
        }
    }
}