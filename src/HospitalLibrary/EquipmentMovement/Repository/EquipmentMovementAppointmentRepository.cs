using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Common;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.EquipmentMovement.Repository
{
    public class EquipmentMovementAppointmentRepository: GenericRepository<EquipmentMovementAppointment>, IEquipmentMovementAppointmentRepository
    {
        public EquipmentMovementAppointmentRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<EquipmentMovementAppointment>> GetAllMovementAppointmentsForRoom(Guid id)
        {
            return await DbSet.Where(x => x.DestinationRoomId == id || x.OriginalRoomId == id)
                .ToListAsync();
        }
    }
}