using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;


namespace HospitalLibrary.Patients.Repository
{
    public interface IPatientAdmissionRepository: IGenericRepository<PatientAdmission>
    {
        Task<List<PatientAdmission>> GetAllPatientAdmissions(); 
        Task<PatientAdmission> GetPatientAdmissionByIdAsync(Guid id);
        Task<List<PatientAdmission>> GetAllHospitalized(); 
    }
}