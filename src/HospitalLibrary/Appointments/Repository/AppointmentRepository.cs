using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Appointments.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}