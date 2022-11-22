using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Patients.Model;


namespace HospitalLibrary.Patients.Repository
{
    public interface IPatientAdmissionRepository: IGenericRepository<PatientAdmission>
    {
        Task<List<PatientAdmission>> GetAllPatientAdmissions(); 
    }
}