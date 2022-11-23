using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Settings;
using HospitalLibrary.TreatmentReports.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.TreatmentReports.Repository
{
    public class TreatmentReportRepository : GenericRepository<TreatmentReport>, ITreatmentReportRepository
    {
        public TreatmentReportRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TreatmentReport> FindByPatientAdmission(Guid id)
        {
            return await DbSet.Include(x => x.BloodPrescriptions)
                .Include(x => x.MedicinePrescriptions)
                .FirstOrDefaultAsync(x => x.PatientAdmissionId == id);
        }
    }
}