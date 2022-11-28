using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Prescriptions.Model;

namespace HospitalLibrary.Prescriptions.Repository
{
    public interface IBloodPrescriptionRepository : IGenericRepository<BloodPrescription>
    {
        Task<BloodPrescription> GetBloodById(Guid id);
    }
}