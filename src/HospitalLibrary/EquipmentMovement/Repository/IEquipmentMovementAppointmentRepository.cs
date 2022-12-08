using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.EquipmentMovement.Model;

namespace HospitalLibrary.EquipmentMovement.Repository
{
    public interface IEquipmentMovementAppointmentRepository: IGenericRepository<EquipmentMovementAppointment>
    {
        Task<List<EquipmentMovementAppointment>> GetAllMovementAppointmentsForRoom(Guid id);
        Task<List<EquipmentMovementAppointment>> GetAllMovementAppointmentByRoomId(Guid originalRoomId);
    }
}