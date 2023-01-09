using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public interface IRoomEventService
    {
        Task<IEnumerable<RoomEvent>> GetAll();
    }
}