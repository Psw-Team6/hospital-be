using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Doctors.Repository
{
    public class DoctorRepository :GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        public async Task<List<Doctor>> GetAllDoctors()
        {
           return await  DbSet.Include(d => d.Room)
                                .Include(d => d.Address)
                                .Include(d => d.Appointments)
                                .ToListAsync();
        }

        public async Task<WorkingSchedule> GetDoctorWorkingSchedule(Guid doctorId)
        {
            return await DbSet.Where(doctor1 => doctor1.Id == doctorId).
                Include( d => d.WorkingSchedule).
                Select(d => d.WorkingSchedule).
                FirstOrDefaultAsync();
        }

        public async Task<Doctor> GetByUsername(string username)
        {
            return await DbSet.FirstOrDefaultAsync(doctor => doctor.Username == username);
        }

        public async Task<List<Doctor>> GetAllDoctorsBySpecialization()
        {
            return await DbSet.Include(d => d.Specialization)
                .Include(d => d.Patients)
                .Where(d => d.Specialization.Name.Equals("General"))
                .ToListAsync();
        }

        public async Task<List<Doctor>> GetBySpecificSpecialisation(String specialization)
        {
            return await DbSet.Include(d => d.Specialization)
                .Where(d => d.Specialization.Name.Equals(specialization))
                .ToListAsync();
        }
    }
}