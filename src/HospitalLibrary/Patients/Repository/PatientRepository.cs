using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Patients.Repository
{
    public class PatientRepository:GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}