using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Enums;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.EquipmentMovement.Repository;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.sharedModel;
using SendGrid.Helpers.Errors.Model;

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

        public async Task<List<EquipmentMovementAppointment>> GetAllByRoomId(Guid id)
        {
            return await _unitOfWork.GetRepository<EquipmentMovementAppointmentRepository>().GetAllMovementAppointmentsForRoom(id);
        }
        
        public async  Task<EquipmentMovementAppointment> GetById(Guid id)
        {
            var equipmentMovementAppointment = await _unitOfWork.EquipmentMovementAppointmentRepository.GetByIdAsync(id);
            return equipmentMovementAppointment;
        }

        public async Task<EquipmentMovementAppointment> Create(EquipmentMovementAppointment equipmentMovementAppointment)
        {
            if (await ValidateAppointment(equipmentMovementAppointment) == false)
            {
                Console.WriteLine("PUCAM ZBOG OVOG SRANJA");
                return null;
            }
            equipmentMovementAppointment.DestinationRoom = await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementAppointment.DestinationRoomId);
            equipmentMovementAppointment.OriginalRoom = await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementAppointment.OriginalRoomId);

            var equipmentMovementAppointmentResult = await _unitOfWork.EquipmentMovementAppointmentRepository.CreateAsync(equipmentMovementAppointment);
            
            await _unitOfWork.CompleteAsync();
            return equipmentMovementAppointmentResult;
        }

        public async  Task<List<EquipmentMovementAppointment>> GetAllAvailableAppointmentsForEquipmentMovement(EquipmentMovementRequest equipmentAppointmentsRequest)
        {
            Console.WriteLine("POGODJEN!");
            if (await ValidateRequest(equipmentAppointmentsRequest) == false)
            {
                Console.WriteLine("PUCA ZBOG OVOGA!");
                return null;
            }
            
            List<EquipmentMovementAppointment> potentialAppointments = await GetAppointmentsForEvery15Min(equipmentAppointmentsRequest);
            potentialAppointments = await DeleteConflictsWithRoomAppointments(potentialAppointments, equipmentAppointmentsRequest.DestinationRoomId);
            potentialAppointments = await DeleteConflictsWithRoomAppointments(potentialAppointments, equipmentAppointmentsRequest.OriginalRoomId);
            potentialAppointments = await DeleteConflictsWithOtherMovementAppointments(potentialAppointments, equipmentAppointmentsRequest.OriginalRoomId);
            potentialAppointments.RemoveAll(x => potentialAppointments.IndexOf(x) > 19);
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
                    EquipmentId = equipmentAppointmentsRequest.EquipmentId,
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
                        < alreadyMadeAppointment.Duration.To))
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }
                    if ((potentialAppointment.Duration.To >= alreadyMadeAppointment.Duration.From) &&  (potentialAppointment.Duration.To < alreadyMadeAppointment.Duration.To))
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
        
        private async Task<List<EquipmentMovementAppointment>> DeleteConflictsWithOtherMovementAppointments(List<EquipmentMovementAppointment> listEquipmentAppointmentsRequest,Guid roomId)
        {
            List<EquipmentMovementAppointment> appointments = await GetAllByRoomId(roomId);
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
                         < alreadyMadeAppointment.Duration.To))
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }
                    
                    if ((potentialAppointment.Duration.To >= alreadyMadeAppointment.Duration.From) &&  (potentialAppointment.Duration.To < alreadyMadeAppointment.Duration.To))
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

        private async Task<bool> ValidateRequest(EquipmentMovementRequest equipmentMovementRequest)
        {
            if (!equipmentMovementRequest.DatesForSearch.IsValidRange())
            {
                return false;
            }

            if (equipmentMovementRequest.DatesForSearch.IsBeforeDate())
            {
                return false;
            }
            
            if (equipmentMovementRequest.Amount <= 0)
            {
                return false;
            }

            RoomEquipment foundEquipment = await _unitOfWork.EquipmentRepository.GetEquipmentById(equipmentMovementRequest.EquipmentId);

            if (foundEquipment == null)
            {
                return false;
            }

            if (foundEquipment.Amount < equipmentMovementRequest.Amount)
            {
                return false;
            }

            Room foundDestinationRoom = await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementRequest.DestinationRoomId);
            Room foundOriginRoom = await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementRequest.OriginalRoomId);

            if (foundDestinationRoom == null || foundOriginRoom == null)
            {
                return false;
            }
            return true;
        }
        
        private async Task<bool> ValidateAppointment(EquipmentMovementAppointment equipmentMovementAppointment)
        {
            if (!equipmentMovementAppointment.Duration.IsValidRange())
            {
                return false;
            }
            
            if (equipmentMovementAppointment.Duration.IsBeforeDate())
            {
                return false;
            }
            
            if (equipmentMovementAppointment.Amount <= 0)
            {
                return false;
            }
            
            RoomEquipment foundEquipment = await _unitOfWork.EquipmentRepository.GetEquipmentById(equipmentMovementAppointment.EquipmentId);

            if (foundEquipment == null)
            {
                return false;
            }

            if (foundEquipment.Amount < equipmentMovementAppointment.Amount)
            {
                return false;
            }

            Room foundDestinationRoom = await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementAppointment.DestinationRoomId);
            Room foundOriginRoom = await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementAppointment.OriginalRoomId);

            if (foundDestinationRoom == null || foundOriginRoom == null)
            {
                return false;
            }
            
            return true;
        }
    }
}