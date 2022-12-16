using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Examinations.Repository
{
    public class SymptomRepository:GenericRepository<Symptom>,ISymptomRepository
    {
        public SymptomRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}