using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Enums;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repository
{
    public class EquipmentRepository:GenericRepository<RoomEquipment>,IIEquipmentRepository
    {

        public EquipmentRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            
        }
        
            
        
        public async Task<List<RoomEquipment>> GetAllEquipment()
        {
            return await  DbSet.ToListAsync();
        }
            
        public async Task<RoomEquipment> GetEquipmentById(Guid roomEquipmentId) 
        {
            return await DbSet.FirstOrDefaultAsync(roomEquipment => roomEquipment.RoomEquipmentId == roomEquipmentId);

        }
        
        
        public async Task<List<RoomEquipment>> GetAllEquipmentByRoomId(Guid roomId)
        {
            return await  DbSet.Where(roomEquipment => roomEquipment.RoomId == roomId)
                .ToListAsync();
        }
       
        public async Task<List<RoomEquipment>> SearchEquipmentByName(string equipmentName)
        {
            return await  DbSet.Where(roomEquipment => roomEquipment.EquipmentName == equipmentName)
                .ToListAsync();
        }
    }
}