using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Common;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.EquipmentMovement.Repository
{
    public class EquipmentMovementAppointmentRepository: GenericRepository<EquipmentMovementAppointment>, IEquipmentMovementAppointmentRepository
    {
        public EquipmentMovementAppointmentRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        
    }
}