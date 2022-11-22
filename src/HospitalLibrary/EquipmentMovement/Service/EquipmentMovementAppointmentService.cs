using System;
using System.Collections.Generic;
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

        public async  Task<List<EquipmentMovementAppointment>> GetAllAvailableAppointmentsForEquipmentMovement(EquipmentMovementRequest equipmentAppointmentsRequest)
        {
            List<EquipmentMovementAppointment> potentialAppointments = await GetAppointmentsForEvery15Min(equipmentAppointmentsRequest);


            return potentialAppointments;
        }

        private async Task<List<EquipmentMovementAppointment>> GetAppointmentsForEvery15Min(EquipmentMovementRequest equipmentAppointmentsRequest)
        {
            List<EquipmentMovementAppointment> potentialAppointments = new List<EquipmentMovementAppointment>();
            DateTime startOfMovement= equipmentAppointmentsRequest.DatesForSearch.From;
            DateTime endOfMovement = equipmentAppointmentsRequest.DatesForSearch.From + equipmentAppointmentsRequest.DurationOfEquipmentMovementAppointment;
            
            while(startOfMovement < equipmentAppointmentsRequest.DatesForSearch.To || endOfMovement < equipmentAppointmentsRequest.DatesForSearch.To)
            {
                EquipmentMovementAppointment newEquipmentMovement = new EquipmentMovementAppointment();
                newEquipmentMovement.DurationOfEquipmentMovementAppointment.From = startOfMovement;
                newEquipmentMovement.DurationOfEquipmentMovementAppointment.To = startOfMovement +
                    equipmentAppointmentsRequest.DurationOfEquipmentMovementAppointment;
               
                startOfMovement= equipmentAppointmentsRequest.DatesForSearch.From;
                endOfMovement = equipmentAppointmentsRequest.DatesForSearch.From + equipmentAppointmentsRequest.DurationOfEquipmentMovementAppointment;

            }

            return potentialAppointments;
        }
    }
}