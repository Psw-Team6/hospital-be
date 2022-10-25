using System.Collections.Generic;
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
           return await  DbSet.Include(d => d.Room).ToListAsync();
        }
    }
}