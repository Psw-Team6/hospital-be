using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Patients.Repository
{
    public class PatientAdmissionRepository:GenericRepository<PatientAdmission>, IPatientAdmissionRepository
    {
        public PatientAdmissionRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}