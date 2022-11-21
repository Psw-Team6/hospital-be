using HospitalLibrary.Common;
using HospitalLibrary.Settings;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalLibrary.TreatmentReports.Repository
{
    public class TreatmentReportRepository : GenericRepository<TreatmentReport>, ITreatmentReportRepository
    {
        public TreatmentReportRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}