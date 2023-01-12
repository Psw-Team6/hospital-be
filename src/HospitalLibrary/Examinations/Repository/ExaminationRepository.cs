using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
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
            return  await DbSet.Select(x => x)
                .Include(x => x.Prescriptions)
                .ThenInclude(p => p.Medicines)
                .Include(x => x.Symptoms)
                .Include(x => x.Appointment).ToListAsync();

            // var exeminationsWithMedicines = exeminations.Select(e =>
            //     new
            //     {
            //         Exemination =e,
            //         Medicines = e.Prescriptions.Select(p => p.Medicines)
            //     });
            // return exeminationsWithMedicines.Select(x => x.Exemination);
        }

        public async Task<List<Examination>> GetExaminationsBySpecializations(Guid specializationId)
        {
            return await DbSet.Where(x => x.Appointment.Doctor.Specialization.Id == specializationId)
                .ToListAsync();
        }
    }
}