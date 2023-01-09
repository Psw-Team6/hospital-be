using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Rooms.Repository
{
    public class RoomEventRepository:GenericRepository<RoomEvent>, IRoomEventRepository
    {
        public RoomEventRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        
    }
}