using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Repository;

namespace HospitalLibrary.Rooms.Service
{
    public class EquipmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EquipmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoomEquipment>> GetAllEquipment()
        {
            return await _unitOfWork.EquipmentRepository.GetAllEquipment();
        }
        
        public async Task<IEnumerable<RoomEquipment>> GetAllEquipmentByRoomId(Guid roomId)
        {
            return await _unitOfWork.EquipmentRepository.GetAllEquipmentByRoomId(roomId);
        }
        
        public async Task<IEnumerable<RoomEquipment>> SearchEquipmentByName(string equipmentName) //vamo stavi string Eq
        {
            //string equipmentName = Eq.Trim().ToLower();
            return await _unitOfWork.EquipmentRepository.SearchEquipmentByName(equipmentName);
        }
        
        public async Task<RoomEquipment> AllEquipmentById(Guid roomEquipmentId)
        {
            return await _unitOfWork.GetRepository<EquipmentRepository>().GetByIdAsync(roomEquipmentId); 
           
        }
        
        
       
        
        
        
    }
}