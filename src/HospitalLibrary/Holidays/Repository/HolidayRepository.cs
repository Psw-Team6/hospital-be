using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Holidays.Repository
{
    public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Holiday>> GetAllHolidaysForDoctor(Guid doctorId)
        {
            return await DbSet.Where(holiday => holiday.DoctorId == doctorId)
                .ToListAsync();
        }
    }
}