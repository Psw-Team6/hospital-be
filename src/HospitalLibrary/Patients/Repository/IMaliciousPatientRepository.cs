using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Patients.Repository
{
    public interface IMaliciousPatientRepository: IGenericRepository<MaliciousPatient>
    {
        Task<List<MaliciousPatient>> GetAllMaliciousPatients();
        
        Task<MaliciousPatient> GetByPatientId(Guid patientId);
    }
}