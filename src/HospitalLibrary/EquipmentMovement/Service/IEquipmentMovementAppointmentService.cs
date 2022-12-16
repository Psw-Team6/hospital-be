using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.EquipmentMovement.Model;

namespace HospitalLibrary.EquipmentMovement.Service
{
    public interface IEquipmentMovementAppointmentService
    {
        public Task CheckAllAppointmentTimes();
        public Task<EquipmentMovementAppointment> GetById(Guid id);
        public Task<List<EquipmentMovementAppointment>> GetAllByRoomId(Guid id);
        
        public Task<EquipmentMovementAppointment> Create(EquipmentMovementAppointment equipmentMovementAppointment);

        public Task<List<EquipmentMovementAppointment>> GetAllAvailableAppointmentsForEquipmentMovement(
            EquipmentMovementRequest equipmentAppointmentsRequest);
        
        public Task<List<EquipmentMovementAppointment>> GetAllMovementAppointmentByRoomId(Guid originalRoomId);

        public Task<bool> DeleteById(EquipmentMovementAppointment equipmentMovementAppointment);

        // public Task<bool> DeleteById(Guid id);
    }
}