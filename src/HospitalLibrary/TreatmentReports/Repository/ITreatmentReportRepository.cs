using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalLibrary.TreatmentReports.Repository
{
    public interface ITreatmentReportRepository : IGenericRepository<TreatmentReport>
    {
        Task<TreatmentReport> FindByPatientAdmission(Guid id);

    }
}