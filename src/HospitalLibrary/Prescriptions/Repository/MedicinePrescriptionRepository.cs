using HospitalLibrary.Common;
using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Prescriptions.Repository
{
    public class MedicinePrescriptionRepository  :GenericRepository<MedicinePrescription>, IMedicinePrescriptionRepository
    {
        protected MedicinePrescriptionRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}