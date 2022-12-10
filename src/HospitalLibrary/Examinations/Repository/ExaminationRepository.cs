using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Examinations.Repository
{
    public class ExaminationRepository:GenericRepository<Examination>,IExaminationRepository
    {
        public ExaminationRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}