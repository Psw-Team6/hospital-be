using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Appointments.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsForDoctor(Guid doctorId)
        {
           return await DbSet.Where(x => x.DoctorId == doctorId)
                .Include(x => x.Duration).Include(x => x.Patient)
                .ToListAsync();
        }
    }
}