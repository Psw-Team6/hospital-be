using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Enums;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.EquipmentMovement.Repository;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;
using Microsoft.EntityFrameworkCore.Update;
using SendGrid.Helpers.Errors.Model;

namespace HospitalLibrary.EquipmentMovement.Service
{
    public class EquipmentMovementAppointmentService :IEquipmentMovementAppointmentService
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
            return await _unitOfWork.EquipmentMovementAppointmentRepository
                .GetAllMovementAppointmentsForRoom(id);
        }

        public async Task<EquipmentMovementAppointment> GetById(Guid id)
        {
            var equipmentMovementAppointment =
                await _unitOfWork.EquipmentMovementAppointmentRepository.GetByIdAsync(id);
            return equipmentMovementAppointment;
        }

        public async Task<EquipmentMovementAppointment> Create(
            EquipmentMovementAppointment equipmentMovementAppointment)
        {
            try
            {
                if (await ValidateAppointment(equipmentMovementAppointment) == false)
                {
                    Console.WriteLine("PUKLO");
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            var equipmentMovementAppointmentResult =
                await _unitOfWork.EquipmentMovementAppointmentRepository.CreateAsync(equipmentMovementAppointment);

            await _unitOfWork.CompleteAsync();
            Console.WriteLine("SKIDam opremu");
            RoomEquipment e =
                await _unitOfWork.EquipmentRepository.GetEquipmentById(equipmentMovementAppointment.EquipmentId);

            Console.WriteLine(e.Amount + " pocetni amount");
            e.Amount -= equipmentMovementAppointment.Amount;
            Console.WriteLine(e.Amount + " posle amount");
            await _unitOfWork.EquipmentRepository.UpdateAsync(e);
            await _unitOfWork.CompleteAsync();
            e = await _unitOfWork.EquipmentRepository.GetEquipmentById(equipmentMovementAppointment.EquipmentId);
            Console.WriteLine(e.Amount + " nakon updatea amount");

            return equipmentMovementAppointmentResult;
        }

        public async Task<List<EquipmentMovementAppointment>> GetAllAvailableAppointmentsForEquipmentMovement(
            EquipmentMovementRequest equipmentAppointmentsRequest)
        {
            Console.WriteLine("POGODJEN!");
            try
            {
                if (await ValidateRequest(equipmentAppointmentsRequest) == false)
                {
                    Console.WriteLine("PUCA ZBOG OVOGA!");
                    return null;
                }
            }
            catch
            {
                return null;
                
            }

            List<EquipmentMovementAppointment> potentialAppointments =
                await GetAppointmentsForEvery15Min(equipmentAppointmentsRequest);
            potentialAppointments = await DeleteConflictsWithRoomAppointments(potentialAppointments,
                equipmentAppointmentsRequest.DestinationRoomId);
            potentialAppointments = await DeleteConflictsWithRoomAppointments(potentialAppointments,
                equipmentAppointmentsRequest.OriginalRoomId);
            potentialAppointments = await DeleteConflictsWithOtherMovementAppointments(potentialAppointments,
                equipmentAppointmentsRequest.OriginalRoomId);
            potentialAppointments.RemoveAll(x => potentialAppointments.IndexOf(x) > 19);
            return potentialAppointments;
        }

        private async Task<List<EquipmentMovementAppointment>> GetAppointmentsForEvery15Min(
            EquipmentMovementRequest equipmentAppointmentsRequest)
        {
            List<EquipmentMovementAppointment> potentialAppointments = new List<EquipmentMovementAppointment>();
            DateTime startOfMovement = equipmentAppointmentsRequest.DatesForSearch.From;
            DateTime endOfMovement = startOfMovement + equipmentAppointmentsRequest.Duration;

            while (startOfMovement < equipmentAppointmentsRequest.DatesForSearch.To ||
                   endOfMovement < equipmentAppointmentsRequest.DatesForSearch.To)
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

            return await Task.FromResult(potentialAppointments);
        }

        private async Task<List<EquipmentMovementAppointment>> DeleteConflictsWithRoomAppointments(
            List<EquipmentMovementAppointment> listEquipmentAppointmentsRequest, Guid roomId)
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

                    if ((potentialAppointment.Duration.To >= alreadyMadeAppointment.Duration.From) &&
                        (potentialAppointment.Duration.To < alreadyMadeAppointment.Duration.To))
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

        private async Task<List<EquipmentMovementAppointment>> DeleteConflictsWithOtherMovementAppointments(
            List<EquipmentMovementAppointment> listEquipmentAppointmentsRequest, Guid roomId)
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

                    if ((potentialAppointment.Duration.To >= alreadyMadeAppointment.Duration.From) &&
                        (potentialAppointment.Duration.To < alreadyMadeAppointment.Duration.To))
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
                Console.WriteLine("Request end is before start.");
                return false;
            }

            if ( equipmentMovementRequest.DatesForSearch.From.Date.Date < DateTime.Now.Date || equipmentMovementRequest.DatesForSearch.To.Date.Date < DateTime.Now)
            {
                Console.WriteLine("Request cant start before today!");
                return false;
            }

            if (equipmentMovementRequest.Amount <= 0)
            {
                Console.WriteLine("Equipment amount is negative!");
                return false;
            }

            RoomEquipment foundEquipment =
                await _unitOfWork.EquipmentRepository.GetEquipmentById(equipmentMovementRequest.EquipmentId);

            if (foundEquipment == null)
            {
                Console.WriteLine("Equipment not found!");
                return false;
            }

            if (foundEquipment.Amount < equipmentMovementRequest.Amount)
            {
                Console.WriteLine("Equipment amount too large!");
                return false;
            }

            Room foundDestinationRoom =
                await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementRequest.DestinationRoomId);
            Room foundOriginRoom =
                await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementRequest.OriginalRoomId);

            if (foundDestinationRoom == null)
            {
                Console.WriteLine("Destination room not found!");
                return false;
            }

            if (foundOriginRoom == null)
            {
                Console.WriteLine("Origin room not found!");
                return false;
            }

            if (foundOriginRoom == foundDestinationRoom)
            {
                Console.WriteLine("Destination and origin room are same!");
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateAppointment(EquipmentMovementAppointment equipmentMovementAppointment)
        {
            if (!equipmentMovementAppointment.Duration.IsValidRange())
            {
                Console.WriteLine("Request end is before start.");
                return false;
            }

            if (equipmentMovementAppointment.Duration.From.Date < DateTime.Now || equipmentMovementAppointment.Duration.To.Date < DateTime.Now)
            {
                Console.WriteLine("Request cant start before today!");
                return false;
            }

            if (equipmentMovementAppointment.Amount <= 0)
            {
                Console.WriteLine("Equipment amount is negative!");
                return false;
            }

            RoomEquipment foundEquipment =
                await _unitOfWork.EquipmentRepository.GetEquipmentById(equipmentMovementAppointment.EquipmentId);

            if (foundEquipment == null)
            {
                Console.WriteLine("Equipment not found!");
                return false;
            }

            if (foundEquipment.Amount < equipmentMovementAppointment.Amount)
            {
                Console.WriteLine("Equipment amount too large!");
                return false;
            }

            Room foundDestinationRoom =
                await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementAppointment.DestinationRoomId);
            Room foundOriginRoom =
                await _unitOfWork.RoomRepository.GetByIdAsync(equipmentMovementAppointment.OriginalRoomId);

            if (foundDestinationRoom == null)
            {
                Console.WriteLine("Destination room not found!");
                return false;
            }

            if (foundOriginRoom == null)
            {
                Console.WriteLine("Origin room not found!");
                return false;
            }

            return true;
        }

        public async Task CheckAllAppointmentTimes()
        {
            IEnumerable<EquipmentMovementAppointment> allAppointments = await _unitOfWork.EquipmentMovementAppointmentRepository.GetAllAsync();
            var equipmentMovementAppointments = allAppointments as EquipmentMovementAppointment[] ?? allAppointments.ToArray();
            if(!equipmentMovementAppointments.Any())
                return;
            
            foreach (var appointment in equipmentMovementAppointments)
            {
                if (appointment.Duration.To < DateTime.Now)
                {
                    List<RoomEquipment> currentRoomEquipmentInRoom = await _unitOfWork.EquipmentRepository.GetAllEquipmentByRoomId(appointment.DestinationRoomId);

                    bool found = false;
                    //Check if room has that equipment
                    foreach (var equipmentInRoom in currentRoomEquipmentInRoom)
                    {
                        if (equipmentInRoom.EquipmentName == appointment.EquipmentName)
                        {
                            found = true;
                            equipmentInRoom.Amount += appointment.Amount;
                            await _unitOfWork.EquipmentRepository.UpdateAsync(equipmentInRoom);
                            await _unitOfWork.CompleteAsync();
                            break;
                        }
                    }

                    if (!found) //If no equipment in room make new
                    {
                        RoomEquipment newRoomEquipment = new RoomEquipment();
                        newRoomEquipment.EquipmentName = appointment.EquipmentName;
                        newRoomEquipment.Amount = appointment.Amount;
                        newRoomEquipment.RoomId = appointment.DestinationRoomId;
                        await _unitOfWork.EquipmentRepository.CreateAsync(newRoomEquipment);
                        await _unitOfWork.CompleteAsync();
                    }
                    
                    //Delete appointment
                    await _unitOfWork.EquipmentMovementAppointmentRepository.DeleteAsync(appointment);
                    await _unitOfWork.CompleteAsync();
                }
            }
        }
    }
}