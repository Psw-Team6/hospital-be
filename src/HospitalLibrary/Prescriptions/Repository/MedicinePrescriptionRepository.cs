using HospitalLibrary.Common;
using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Prescriptions.Repository
{
    public class MedicinePrescriptionRepository  :GenericRepository<MedicinePrescription>, IMedicinePrescriptionRepository
    {
        public MedicinePrescriptionRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}