using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalLibrary.TreatmentReports.Service
{
    public class TreatmentReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TreatmentReport> CreateTreatmentReport(TreatmentReport report)
        {
            var newReport = await _unitOfWork.TreatmentReportRepository.CreateAsync(report);
            await _unitOfWork.CompleteAsync();
            return newReport;
        }
    }
}