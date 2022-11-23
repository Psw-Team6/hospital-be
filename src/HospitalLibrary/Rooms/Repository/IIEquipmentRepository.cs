using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Repository
{
    public interface IIEquipmentRepository:IGenericRepository<RoomEquipment>
    {
        Task<List<RoomEquipment>>GetAllEquipment();
      
        Task<RoomEquipment> GetEquipmentById(Guid roomEquipmentId);
        
        Task<List<RoomEquipment>> GetAllEquipmentByRoomId(Guid roomId);
        
        Task<List<RoomEquipment>> SearchEquipmentByName(string equipmentName);
    }
}