using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Rooms.Repository
{
    public class RoomRepository : GenericRepository<Room>,IRoomRepository
    {
        public RoomRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
