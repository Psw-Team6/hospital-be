using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.EquipmentMovement.Model;

namespace HospitalLibrary.EquipmentMovement.Service
{
    public class EquipmentMovementAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EquipmentMovementAppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<EquipmentMovementAppointment> GetById(Guid id)
        {
            var equipmentMovementAppointment = await _unitOfWork.EquipmentMovementAppointmentRepository.GetByIdAsync(id);
            return equipmentMovementAppointment;
        }

        public async Task<EquipmentMovementAppointment> Create(EquipmentMovementAppointment equipmentMovementAppointment)
        {
            var equipmentMovementApp =await _unitOfWork.EquipmentMovementAppointmentRepository.CreateAsync(equipmentMovementAppointment);
            await _unitOfWork.CompleteAsync();
            return equipmentMovementApp;
        }
    }
}