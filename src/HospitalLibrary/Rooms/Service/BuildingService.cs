using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public class BuildingService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public BuildingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<Building>> GetAll()
        {
            return await _unitOfWork.BuildingRepository.GetAllBuildings();
        }

        public async Task<Building> GetById(Guid id)
        {
            return await _unitOfWork.BuildingRepository.GetByIdAsync(id);
        }

        public async Task<bool> Update(Building building)
        {
            await _unitOfWork.BuildingRepository.UpdateAsync(building);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}