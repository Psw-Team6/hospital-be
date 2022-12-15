using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public interface IRoomRenovationService
    {
        public Task<List<RoomMerging>> GetAllAvailableAppointmentsForRoomMerging(RoomMerging appointmentRequested);
        public Task<List<RoomSpliting>> GetAllAvailableAppointmentsForRoomSpliting(RoomSpliting appointmentRequested);
        public Task<RoomMerging> CreateRoomMerging(RoomMerging roomMerging);
        public Task<RoomSpliting> CreateRoomSpliting(RoomSpliting roomSpliting);
        public Task<RoomSpliting> GetSplitingById(Guid id);
        public Task<RoomMerging> GetMergingById(Guid id);
        public Task CheckIfRenovationFinished();
    }
}