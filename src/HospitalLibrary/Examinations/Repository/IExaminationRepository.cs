using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;

namespace HospitalLibrary.Examinations.Repository
{
    public interface IExaminationRepository:IGenericRepository<Examination>
    {
        Task<Examination> GetExaminationByAppointment(Appointment appointment);
        Task<IEnumerable<Examination>> GetAllExaminations();
    }
}