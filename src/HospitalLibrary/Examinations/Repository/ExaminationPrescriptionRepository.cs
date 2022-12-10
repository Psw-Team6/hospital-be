using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Examinations.Repository
{
    public class ExaminationPrescriptionRepository:GenericRepository<ExaminationPrescription>,IExaminationPrescriptionRepository
    {
        public ExaminationPrescriptionRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}