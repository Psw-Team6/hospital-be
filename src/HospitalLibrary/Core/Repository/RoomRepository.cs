using System;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Common;

namespace HospitalLibrary.Core.Repository
{
    public class RoomRepository : GenericRepository<Room>,IRoomRepository
    {
        public RoomRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
