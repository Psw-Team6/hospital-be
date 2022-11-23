using System.Collections.Generic;
using System.Linq;
﻿using System;
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
        public async Task<List<Patient>> GetAllPatients()
        {
            return await DbSet.Include(p => p.Address)
                .Include(p => p.Feedbacks)
                .Include(p=> p.Allergies)
                .Include(p=> p.Doctor)
                .ToListAsync();
        }

        public async Task<Patient> GetPatientById(Guid id)
        {
            return await DbSet.Include(p => p.Address)
                .Include(p => p.Feedbacks)
                .Include(p=> p.Allergies)
                .Include(p=> p.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}