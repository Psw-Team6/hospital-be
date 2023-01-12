using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Patients.Repository
{
    public interface IPatientHealthStateRepository:IGenericRepository<PatientHealthState>
    {
        public Task<List<PatientHealthState>> GetAllByPatientId(Guid patientId);
    }
}