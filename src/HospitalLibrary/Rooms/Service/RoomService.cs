using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Rooms.Service
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppointmentService _appointmentService;

        public RoomService(IUnitOfWork unitOfWork, IAppointmentService appointmentService)
        {
            _unitOfWork = unitOfWork;
            _appointmentService = appointmentService;
        }

        public RoomService(IUnitOfWork unitOfWork)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _unitOfWork.RoomRepository.GetAllRooms();
        }
        
        public async Task<IEnumerable<Room>> GetAllByBuildingIdAndFloorId(Guid buildingId, Guid floorId)
        {
            return await _unitOfWork.RoomRepository.GetAllRoomsByBuildingIdAndFloorId(buildingId, floorId);
        }

        public async Task<Room> GetById(Guid id)
        {
            return await _unitOfWork.RoomRepository.GetByIdAsync(id);
        }

        public async Task<bool> Update(Room room)
        {
            await _unitOfWork.RoomRepository.UpdateAsync(room);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<Room> MergeRooms(Room room1, Room room2)
        {
            Room newRoom = new Room();
            newRoom = room1;
            //TREBACE OVO VRV foreach (var room2Bed in room2.Beds) newRoom.Beds.Add(room2Bed);
            newRoom.Id = Guid.NewGuid();
            GRoom newGroom = new GRoom();
            
            GRoom groom1 = await _unitOfWork.GRoomRepository.GetByIdAsync(room1.GRoomId);
            GRoom groom2 = await _unitOfWork.GRoomRepository.GetByIdAsync(room2.GRoomId);
            
            newGroom.RoomId = newRoom.Id;
            newGroom.Id = Guid.NewGuid();
            newGroom.PositionX = Math.Min(groom1.PositionX, groom2.PositionX);
            newGroom.PositionY = Math.Min(groom1.PositionY, groom2.PositionY);
            newGroom.Lenght = Math.Max(groom1.PositionX + groom1.Lenght, groom2.PositionX + groom2.Lenght) - newGroom.PositionX;
            newGroom.Width = Math.Max(groom1.PositionY + groom1.Width, groom2.PositionY + groom2.Width) - newGroom.PositionY;

            await _unitOfWork.GRoomRepository.CreateAsync(newGroom);
            await _unitOfWork.RoomRepository.CreateAsync(newRoom);
            await _unitOfWork.GRoomRepository.DeleteAsync(groom1);
            await _unitOfWork.GRoomRepository.DeleteAsync(groom2);
            await _unitOfWork.RoomRepository.DeleteAsync(room1);
            await _unitOfWork.RoomRepository.DeleteAsync(room2);
            await _unitOfWork.CompleteAsync();

            return newRoom;
        }
        
        public async Task<Room> SplitRoom(Room room1)
        {
            Room newRoom1 = new Room();
            newRoom1 = room1;
            newRoom1.Id = Guid.NewGuid();
            
            Room newRoom2 = new Room();
            newRoom2.Id = Guid.NewGuid();
            
            GRoom originalGroom = await _unitOfWork.GRoomRepository.GetByIdAsync(room1.GRoomId);
            
            GRoom newGroom1 = new GRoom();
            newGroom1.RoomId = newRoom1.Id;
            newGroom1.Id = Guid.NewGuid();
            newGroom1.PositionX = originalGroom.PositionX;
            newGroom1.PositionY = originalGroom.PositionY;
            newGroom1.Lenght = originalGroom.Lenght / 2;
            newGroom1.Width = originalGroom.Width / 2;
            
            GRoom newGroom2 = new GRoom();
            newGroom2.RoomId = newRoom2.Id;
            newGroom2.Id = Guid.NewGuid();
            newGroom2.Lenght = originalGroom.Lenght - newGroom1.Lenght;
            newGroom2.Width = originalGroom.Width - newGroom1.Width;
            newGroom2.PositionX = newGroom1.PositionX + newGroom1.Lenght;
            newGroom2.PositionY = newGroom1.PositionY + newGroom1.Width;

            await _unitOfWork.GRoomRepository.CreateAsync(newGroom1);
            await _unitOfWork.GRoomRepository.CreateAsync(newGroom2);
            
            await _unitOfWork.RoomRepository.CreateAsync(newRoom1);
            await _unitOfWork.RoomRepository.CreateAsync(newRoom2);
            
            await _unitOfWork.RoomRepository.DeleteAsync(room1);
            await _unitOfWork.GRoomRepository.DeleteAsync(originalGroom);
            
            await _unitOfWork.CompleteAsync();

            return newRoom1;
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
                    if ((potentialAppointment.DatesForSearch.From
                         >= alreadyMadeAppointment.Duration.From)
                        &&
                        (potentialAppointment.DatesForSearch.From
                         < alreadyMadeAppointment.Duration.To))
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }

                    if ((potentialAppointment.DatesForSearch.To >= alreadyMadeAppointment.Duration.From) &&
                        (potentialAppointment.DatesForSearch.To < alreadyMadeAppointment.Duration.To))
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
                    if ((potentialAppointment.DatesForSearch.From
                         >= alreadyMadeAppointment.Duration.From)
                        &&
                        (potentialAppointment.DatesForSearch.From
                         < alreadyMadeAppointment.Duration.To))
                    {
                        appointmentsToRemove.Add(potentialAppointment);
                        break;
                    }

                    if ((potentialAppointment.DatesForSearch.To >= alreadyMadeAppointment.Duration.From) &&
                        (potentialAppointment.DatesForSearch.To < alreadyMadeAppointment.Duration.To))
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
            DateTime startOfMovement = roomMerging.DatesForSearch.From;
            DateTime endOfMovement = startOfMovement + roomMerging.Duration;

            while (startOfMovement < roomMerging.DatesForSearch.To ||
                   endOfMovement < roomMerging.DatesForSearch.To)
            {
                RoomMerging newEquipmentMovement = new RoomMerging
                {
                    Room1Id = roomMerging.Room1Id,
                    Room2Id = roomMerging.Room2Id,
                    DatesForSearch = new DateRange
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
                    Id = Guid.NewGuid(),
                    RoomId = roomSpliting.RoomId,
                    DatesForSearch = new DateRange
                    {
                        From = startOfMovement,
                        To = endOfMovement
                    }
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
            if (!roomMergingRequest.DatesForSearch.IsValidRange())
            {
                Console.WriteLine("Request end is before start.");
                return false;
            }

            if ( roomMergingRequest.DatesForSearch.From.Date.Date < DateTime.Now.Date || roomMergingRequest.DatesForSearch.To.Date.Date < DateTime.Now)
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

            Room foundRoom1 =
                await _unitOfWork.RoomRepository.GetByIdAsync(roomSplitingRequest.RoomId);

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
