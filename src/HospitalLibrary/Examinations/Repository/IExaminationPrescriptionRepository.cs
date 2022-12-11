using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;

namespace HospitalLibrary.Examinations.Repository
{
    public interface IExaminationPrescriptionRepository:IGenericRepository<ExaminationPrescription>
    {
        Task<ExaminationPrescription> GetPrescriptionById(Guid id);
    }
}