using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.EquipmentMovement.Service
{
    public class EquipmentMovementAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppointmentService _appointmentService;

        public EquipmentMovementAppointmentService(IUnitOfWork unitOfWork, IAppointmentService appointmentService)
        {
            _unitOfWork = unitOfWork;
            _appointmentService = appointmentService;
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
            potentialAppointments = await DeleteConflictsWithRoomAppointments(potentialAppointments, equipmentAppointmentsRequest.DestinationRoomId);
            potentialAppointments = await DeleteConflictsWithRoomAppointments(potentialAppointments, equipmentAppointmentsRequest.OriginalRoomId);

            return potentialAppointments;
        }

        private async Task<List<EquipmentMovementAppointment>> GetAppointmentsForEvery15Min(EquipmentMovementRequest equipmentAppointmentsRequest)
        {
            List<EquipmentMovementAppointment> potentialAppointments = new List<EquipmentMovementAppointment>();
            DateTime startOfMovement= equipmentAppointmentsRequest.DatesForSearch.From;
            DateTime endOfMovement = startOfMovement + equipmentAppointmentsRequest.Duration;
            
            while(startOfMovement < equipmentAppointmentsRequest.DatesForSearch.To || endOfMovement < equipmentAppointmentsRequest.DatesForSearch.To)
            {
                EquipmentMovementAppointment newEquipmentMovement = new EquipmentMovementAppointment
                {
                    DestinationRoomId = equipmentAppointmentsRequest.DestinationRoomId,
                    OriginalRoomId = equipmentAppointmentsRequest.OriginalRoomId,
                    EquipmentName = equipmentAppointmentsRequest.EquipmentName,
                    Amount = equipmentAppointmentsRequest.Amount,
                    Duration = new DateRange
                    {
                        From = startOfMovement,
                        To = endOfMovement
                    }
                };

                potentialAppointments.Add(newEquipmentMovement);
                
                startOfMovement += TimeSpan.FromMinutes(15);
                endOfMovement = startOfMovement + equipmentAppointmentsRequest.Duration;
            }

            return potentialAppointments;
        }
        
        private async Task<List<EquipmentMovementAppointment>> DeleteConflictsWithRoomAppointments(List<EquipmentMovementAppointment> listEquipmentAppointmentsRequest,Guid roomId)
        { 
            List<Appointment> appointments = await _appointmentService.GetAllByRoomId(roomId);
            List<EquipmentMovementAppointment> appointmentsToRemove = new List<EquipmentMovementAppointment>();

            if (appointments == null)
            {
                return listEquipmentAppointmentsRequest;
            }
            foreach (var potentialAppointment in listEquipmentAppointmentsRequest)
            {
                foreach (var alreadyMadeAppointment in appointments)
                {
                    if ((potentialAppointment.Duration.From 
                        >= alreadyMadeAppointment.Duration.From) 
                        &&  
                        (potentialAppointment.Duration.From 
                        <= alreadyMadeAppointment.Duration.To))
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }
                    if ((potentialAppointment.Duration.To >= alreadyMadeAppointment.Duration.From) &&  (potentialAppointment.Duration.To <= alreadyMadeAppointment.Duration.To))
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }
                }
            }

            foreach (var aptr in appointmentsToRemove)
            {
                listEquipmentAppointmentsRequest.Remove(aptr);
            }

            return listEquipmentAppointmentsRequest;
        }
    }
}