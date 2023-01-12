using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public interface IRoomEventService
    {
        Task<IEnumerable<RoomEvent>> GetAll();
        Task<RoomEvent> Create(RoomEvent roomEvent);
        Task<int> SuccesfullMergingCount();
        Task<int> SuccesfullSplitingCount();
        Task<int[]> StepMergingCount();
        Task<int[]> StepSplitingCount();
        Task<int> SchedulingCanceledCount();
        
        Task<IEnumerable<RoomEvent>> GetRoomEventsInLastDay();
        Task<int[]> GetAverageMergningSchedulingTimes();
        Task<int[]> GetAverageMergningStepTimes();
        Task<int[]> GetAverageSplitingSchedulingTimes();
        Task<int[]> GetAverageSplitingStepTimes();
    }
}