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
    public class PatientHealthStateRepository:GenericRepository<PatientHealthState>,IPatientHealthStateRepository
    {
        public PatientHealthStateRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PatientHealthState>> GetAllByPatientId(Guid patientId)
        {
            return await DbSet.Where(state => state.Root.Id == patientId).ToListAsync();
        }
    }
}