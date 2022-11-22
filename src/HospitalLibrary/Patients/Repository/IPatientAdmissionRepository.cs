using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;
using Microsoft.EntityFrameworkCore.Query;


namespace HospitalLibrary.Patients.Repository
{
    public interface IPatientAdmissionRepository: IGenericRepository<PatientAdmission>
    {
       Task<PatientAdmission> GetPatientAdmissionByIdAsync(Guid id);
    }
   
}