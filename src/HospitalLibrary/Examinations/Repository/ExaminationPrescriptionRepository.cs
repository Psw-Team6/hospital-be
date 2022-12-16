using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Examinations.Repository
{
    public class ExaminationPrescriptionRepository:GenericRepository<ExaminationPrescription>,IExaminationPrescriptionRepository
    {
        public ExaminationPrescriptionRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ExaminationPrescription> GetPrescriptionById(Guid id)
        {
            return await DbSet.Where(x => x.Id == id).Include(x=>x.Medicines).FirstAsync();
        }
    }
}