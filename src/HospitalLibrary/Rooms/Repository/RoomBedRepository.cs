using System;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repository
{
    public class RoomBedRepository : GenericRepository<RoomBed>, IRoomBedRepository
    {
        public RoomBedRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}