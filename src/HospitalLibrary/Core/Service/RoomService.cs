using System;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;

namespace HospitalLibrary.Core.Service
{
    public class RoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _unitOfWork.RoomRepository.GetAllAsync();
        }

        public async Task<Room> GetById(Guid id)
        {
            return await _unitOfWork.RoomRepository.GetByIdAsync(id);
        }

          public void Create(Room room)
        {
            _unitOfWork.RoomRepository.CreateAsync(room);
            _unitOfWork.CompleteAsync();
        }

        public void Update(Room room)
        {
            _unitOfWork.RoomRepository.UpdateAsync(room);
            _unitOfWork.CompleteAsync();
        }

        public void Delete(Room room)
        {
            _unitOfWork.RoomRepository.DeleteAsync(room);
            _unitOfWork.CompleteAsync();
        }
    }
}
