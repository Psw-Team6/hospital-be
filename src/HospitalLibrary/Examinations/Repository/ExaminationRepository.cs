using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Examinations.Repository
{
    public class ExaminationRepository:GenericRepository<Examination>,IExaminationRepository
    {
        public ExaminationRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Examination> GetExaminationByAppointment(Appointment appointment)
        {
            return await DbSet.Where(x => x.Appointment == appointment)
                .Include(x=>x.Appointment)
                .Include(x => x.Prescriptions).
                Include(x=> x.Symptoms).FirstAsync();
        }

        public async Task<IEnumerable<Examination>> GetAllExaminations()
        {
            return await DbSet.Select(x => x)
                .Include(x => x.Prescriptions)
                .Include(x => x.Symptoms)
                .Include(x => x.Appointment).ToListAsync();
        }
    }
}