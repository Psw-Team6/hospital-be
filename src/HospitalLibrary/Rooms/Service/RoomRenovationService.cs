using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Rooms.Service
{
    public class RoomRenovationService:IRoomRenovationService
    {
        
        private readonly IAppointmentService _appointmentService;

        private readonly IRoomService _roomService;
        private readonly IUnitOfWork _unitOfWork;

        public RoomRenovationService(IRoomService roomService, IUnitOfWork unitOfWork, IAppointmentService appointmentService)
        {
            _roomService = roomService;
            _unitOfWork = unitOfWork;
            _appointmentService = appointmentService;
        }
        
        
        public async Task<List<RoomSpliting>> GetAllSplittingByRoomId(Guid roomId)
        {
            return await _unitOfWork.RoomSplitingRepository.GetAllSplittingByRoomId(roomId);
        }
        
        public async Task<bool> DeleteSplitting(RoomSpliting roomSplitting)
        {
            if (CancelSplitting(roomSplitting))
            {
                await _unitOfWork.RoomSplitingRepository.DeleteAsync(roomSplitting);
                await _unitOfWork.CompleteAsync();
                return true;
            }

            return false;
        }
        
        private bool CancelSplitting(RoomSpliting roomSplitting)
        {
            if(DateTime.Now.AddDays(1).CompareTo(roomSplitting.DatesForSearch.From) < 0)
                return true;
            return false;
        }
        
        public async Task<List<RoomMerging>> GetAllMergingByRoomId(Guid originalRoomId)
        {
            return await _unitOfWork.RoomMergingRepository.GetAllMergingByRoomId(originalRoomId);
        }
        
        public async Task<bool> DeleteMerging(RoomMerging roomMerging)
        {
            if (CancelMerging(roomMerging))
            {
                await _unitOfWork.RoomMergingRepository.DeleteAsync(roomMerging);
                await _unitOfWork.CompleteAsync();
                return true;
            }

            return false;
        }
        
        private bool CancelMerging(RoomMerging roomMerging)
        {
            if(DateTime.Now.AddDays(1).CompareTo(roomMerging.DateRangeOfMerging.From) < 0)
                return true;
            return false;
        }
        
        public async Task<RoomMerging> CreateRoomMerging(RoomMerging roomMerging)
        {
            try
            {
                if (await ValidateMergingRequest(roomMerging) == false)
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

            var roomMergingResult = await _unitOfWork.RoomMergingRepository.CreateAsync(roomMerging);

            await _unitOfWork.CompleteAsync();

            return roomMergingResult;
        }

        public async Task<RoomSpliting> CreateRoomSpliting(RoomSpliting roomSpliting)
        {
            try
            {
                if (await ValidateSplitingRequest(roomSpliting) == false)
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

            var roomSplitingResult = await _unitOfWork.RoomSplitingRepository.CreateAsync(roomSpliting);

            await _unitOfWork.CompleteAsync();

            return roomSplitingResult;
        }

        public Task<RoomSpliting> GetSplitingById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RoomMerging> GetMergingById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task CheckIfRenovationFinished()
        {
            
            IEnumerable<RoomMerging> allMergingAppointments = await _unitOfWork.RoomMergingRepository.GetAllAsync();
            var allMergingAppointmentsArray = allMergingAppointments as RoomMerging[] ?? allMergingAppointments.ToArray();
            if (allMergingAppointmentsArray.Any())
            {

                foreach (var appointment in allMergingAppointmentsArray)
                {
                    if (appointment.DateRangeOfMerging.To < DateTime.Now)
                    {
                        Console.WriteLine("MERGING ROOMS!");
                        await _roomService.MergeRooms(appointment.Room1Id, appointment.Room2Id);
                        await  _unitOfWork.RoomMergingRepository.DeleteAsync(appointment);
                        Console.WriteLine("DELETED OLD APPOINTMENT");
                        await _unitOfWork.CompleteAsync();
                        break;
                    }
                }
            }

            IEnumerable<RoomSpliting> allSplitingAppointments = await _unitOfWork.RoomSplitingRepository.GetAllAsync();
            if(!allSplitingAppointments.Any())
                return;
            var allSplitingAppointmentsArray = allSplitingAppointments as RoomSpliting[] ?? allSplitingAppointments.ToArray();

            
            foreach (var appointment in allSplitingAppointmentsArray)
            {
                if (appointment.DatesForSearch.To < DateTime.Now)
                {
                    Console.WriteLine("SPLITING ROOMS!");
                    await _roomService.SplitRoom(appointment.RoomId, appointment.newRoomName);
                    
                    Console.WriteLine("ROOM SPLITED!");
                    await  _unitOfWork.RoomSplitingRepository.DeleteAsync(appointment);
                    Console.WriteLine("ROOM SPLITING DELETED!");
                    await _unitOfWork.CompleteAsync();
                    break;
                    
                }
            }
        }

        public async Task<List<RoomMerging>> GetAllAvailableAppointmentsForRoomMerging(RoomMerging roomMergingRequest)
        {
            Console.WriteLine("POGODJEN!");
            try
            {
                if (await ValidateMergingRequest(roomMergingRequest) == false)
                {
                    Console.WriteLine("PUCA ZBOG OVOGA!");
                    return null;
                }
            }
            catch
            {
                return null;
                
            }

            List<RoomMerging> potentialAppointments =
                await GetAppointmentsForEvery15MinMerging(roomMergingRequest);
            potentialAppointments = await DeleteConflictsWithRoomAppointmentsMerging(potentialAppointments,
                roomMergingRequest.Room1Id);
            potentialAppointments = await DeleteConflictsWithRoomAppointmentsMerging(potentialAppointments,
                roomMergingRequest.Room2Id);
            potentialAppointments.RemoveAll(x => potentialAppointments.IndexOf(x) > 19);
            return potentialAppointments;
        }
        private async Task<List<RoomMerging>> DeleteConflictsWithRoomAppointmentsMerging(List<RoomMerging> listRoomMergings, Guid roomId)
        {
            List<Appointment> appointments = await _appointmentService.GetAllByRoomId(roomId);
            List<RoomMerging> appointmentsToRemove = new List<RoomMerging>();

            if (appointments == null)
            {
                return listRoomMergings;
            }

            foreach (var potentialAppointment in listRoomMergings)
            {
                foreach (var alreadyMadeAppointment in appointments)
                {
                    if (potentialAppointment.DateRangeOfMerging.From < alreadyMadeAppointment.Duration.To)
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }

                    if (potentialAppointment.DateRangeOfMerging.To < alreadyMadeAppointment.Duration.To)
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }
                }
            }

            foreach (var aptr in appointmentsToRemove)
            {
                listRoomMergings.Remove(aptr);
            }

            return listRoomMergings;
        }
        private async Task<List<RoomSpliting>> DeleteConflictsWithRoomAppointmentsSpliting(List<RoomSpliting> listRoomSplitings, Guid roomId)
        {
            List<Appointment> appointments = await _appointmentService.GetAllByRoomId(roomId);
            List<RoomSpliting> appointmentsToRemove = new List<RoomSpliting>();

            if (appointments == null)
            {
                return listRoomSplitings;
            }

            foreach (var potentialAppointment in listRoomSplitings)
            {
                foreach (var alreadyMadeAppointment in appointments)
                {
                    if (potentialAppointment.DatesForSearch.From < alreadyMadeAppointment.Duration.To)
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }

                    if (potentialAppointment.DatesForSearch.To < alreadyMadeAppointment.Duration.To)
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }
                }
            }

            foreach (var aptr in appointmentsToRemove)
            {
                listRoomSplitings.Remove(aptr);
            }

            return listRoomSplitings;
        }
        private async Task<List<RoomMerging>> GetAppointmentsForEvery15MinMerging(RoomMerging roomMerging)
        {
            List<RoomMerging> potentialAppointments = new List<RoomMerging>();
            DateTime startOfMovement = roomMerging.DateRangeOfMerging.From;
            DateTime endOfMovement = startOfMovement + roomMerging.Duration;

            while (startOfMovement < roomMerging.DateRangeOfMerging.To ||
                   endOfMovement < roomMerging.DateRangeOfMerging.To)
            {
                RoomMerging newEquipmentMovement = new RoomMerging
                {
                    Room1Id = roomMerging.Room1Id,
                    Room2Id = roomMerging.Room2Id,
                    DateRangeOfMerging = new DateRange
                    {
                        From = startOfMovement,
                        To = endOfMovement
                    }
                };

                potentialAppointments.Add(newEquipmentMovement);

                startOfMovement += TimeSpan.FromMinutes(15);
                endOfMovement = startOfMovement + roomMerging.Duration;
            }

            return await Task.FromResult(potentialAppointments);
        }
        private async Task<List<RoomSpliting>> GetAppointmentsForEvery15MinSpliting(RoomSpliting roomSpliting)
        {
            List<RoomSpliting> potentialAppointments = new List<RoomSpliting>();
            DateTime startOfMovement = roomSpliting.DatesForSearch.From;
            DateTime endOfMovement = startOfMovement + roomSpliting.Duration;

            while (startOfMovement < roomSpliting.DatesForSearch.To ||
                   endOfMovement < roomSpliting.DatesForSearch.To)
            {
                RoomSpliting newRoomSpliting = new RoomSpliting
                {
                    Id = Guid.Empty,
                    RoomId = roomSpliting.RoomId,
                    DatesForSearch = new DateRange
                    {
                        From = startOfMovement,
                        To = endOfMovement
                    },
                    newRoomName = roomSpliting.newRoomName
                };

                potentialAppointments.Add(newRoomSpliting);

                startOfMovement += TimeSpan.FromMinutes(15);
                endOfMovement = startOfMovement + roomSpliting.Duration;
            }

            return await Task.FromResult(potentialAppointments);
        }
        private async Task<bool> ValidateMergingRequest(RoomMerging roomMergingRequest)
        {
            //PROVERI I APPOINTMNETS
            if (!roomMergingRequest.DateRangeOfMerging.IsValidRange())
            {
                Console.WriteLine("Request end is before start.");
                return false;
            }

            if ( roomMergingRequest.DateRangeOfMerging.From.Date.Date < DateTime.Now.Date || roomMergingRequest.DateRangeOfMerging.To.Date.Date < DateTime.Now)
            {
                Console.WriteLine("Request cant start before today!");
                return false;
            }

            Room foundRoom1 =
                await _unitOfWork.RoomRepository.GetByIdAsync(roomMergingRequest.Room1Id);
            Room foundRoom2 =
                await _unitOfWork.RoomRepository.GetByIdAsync(roomMergingRequest.Room2Id);

            if (foundRoom1 == null)
            {
                Console.WriteLine("Room 1 not found!");
                return false;
            }

            if (foundRoom2 == null)
            {
                Console.WriteLine("Room 2 not found!");
                return false;
            }

            if (foundRoom1 == foundRoom2)
            {
                Console.WriteLine("Room1 and Room2 are same!");
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateSplitingRequest(RoomSpliting roomSplitingRequest)
        {
            //PROVERI I APPOINTMNETS
            if (!roomSplitingRequest.DatesForSearch.IsValidRange())
            {
                Console.WriteLine("Request end is before start.");
                return false;
            }

            if ( roomSplitingRequest.DatesForSearch.From.Date.Date < DateTime.Now.Date || roomSplitingRequest.DatesForSearch.To.Date.Date < DateTime.Now)
            {
                Console.WriteLine("Request cant start before today!");
                return false;
            }

            Room foundRoom1 = await _unitOfWork.RoomRepository.GetByIdAsync(roomSplitingRequest.RoomId);

            if (foundRoom1 == null)
            {
                Console.WriteLine("Room 1 not found!");
                return false;
            }
            
            return true;
        }
        public async Task<List<RoomSpliting>> GetAllAvailableAppointmentsForRoomSpliting(RoomSpliting roomSpliting)
        {
            Console.WriteLine("POGODJEN!");
            try
            {
                if (await ValidateSplitingRequest(roomSpliting) == false)
                {
                    Console.WriteLine("PUCA ZBOG OVOGA!");
                    return null;
                }
            }
            catch
            {
                return null;
                
            }

            List<RoomSpliting> potentialAppointments = await GetAppointmentsForEvery15MinSpliting(roomSpliting);
            potentialAppointments = await DeleteConflictsWithRoomAppointmentsSpliting(potentialAppointments,
                roomSpliting.RoomId);
            potentialAppointments.RemoveAll(x => potentialAppointments.IndexOf(x) > 19);
            return potentialAppointments;
        }
    
    }
}