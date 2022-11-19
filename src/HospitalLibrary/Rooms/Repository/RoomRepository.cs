using System;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Repository
{
    public class RoomRepository : GenericRepository<Room>,IRoomRepository
    {
        public RoomRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<List<Room>> GetAllRooms()
        {
            return await  DbSet
                .ToListAsync();
        }
            
        public async Task<List<Room>> GetAllRoomsByBuildingIdAndFloorId(Guid buildingId, Guid floorId)
        {
            return await  DbSet.Where(room1 => room1.BuildingId == buildingId && room1.FloorId == floorId)
                .ToListAsync();
        }
        
        
       
        
    }
}
