using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.sharedModel;

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
            DateTime endOfMovement = startOfMovement + equipmentAppointmentsRequest.DurationOfEquipmentMovementAppointment;
            
            while(startOfMovement < equipmentAppointmentsRequest.DatesForSearch.To || endOfMovement < equipmentAppointmentsRequest.DatesForSearch.To)
            {
                EquipmentMovementAppointment newEquipmentMovement = new EquipmentMovementAppointment
                {
                    DurationOfEquipmentMovementAppointment = new DateRange
                    {
                        From = startOfMovement,
                        To = endOfMovement
                    }
                };

                potentialAppointments.Add(newEquipmentMovement);
                
                startOfMovement += TimeSpan.FromMinutes(15);
                endOfMovement = startOfMovement + equipmentAppointmentsRequest.DurationOfEquipmentMovementAppointment;
            }

            return potentialAppointments;
        }
    }
}