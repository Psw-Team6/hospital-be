using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Rooms.Repository
{
    public class RoomSplitingRepository : GenericRepository<RoomSpliting>, IRoomSplitingRepository
    {
        public RoomSplitingRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}