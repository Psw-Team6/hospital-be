using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Rooms.Repository
{
    public class RoomMerginRepository:GenericRepository<RoomMerging>, IRoomMergingRepository
    {
        public RoomMerginRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}