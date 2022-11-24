using System;
using System.Linq;
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
        
        public async Task<PatientAdmission> GetPatientAdmissionByIdAsync(Guid id)
        {
            return await DbSet.Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<List<PatientAdmission>> GetAllHospitalized()
        {
            return await DbSet.Include(p => p.Patient)
                .Include(p=>p.Patient.Allergies)
                .Where(p=> p.DateOfDischarge == null)
                .ToListAsync();
        }
    }
}