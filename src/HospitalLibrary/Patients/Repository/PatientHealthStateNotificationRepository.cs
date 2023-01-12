using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Patients.Repository
{
    public class PatientHealthStateNotificationRepository:GenericRepository<PatientHealthStateNotification>,IPatientHealthStateNotificationRepository
    {
        public PatientHealthStateNotificationRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}