using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Doctors.Repository
{
    public class WorkingScheduleRepository:GenericRepository<WorkingSchedule>,IWorkingSchueduleRepository
    {
        public WorkingScheduleRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}