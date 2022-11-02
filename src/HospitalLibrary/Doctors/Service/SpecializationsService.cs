using System;
using System.Threading.Tasks;
using AutoMapper;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Doctors.Service
{
    public class SpecializationsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecializationsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Specialization> Create(Specialization specialization)
        {
           // var convert = _mapper.Map<Specialization>(specialization);
            var spec =await _unitOfWork.SpecializationsRepository.CreateAsync(specialization);
            await _unitOfWork.CompleteAsync();
            return spec;
        }

        public async  Task<Specialization> GetById(Guid id)
        {
            var specialization = await _unitOfWork.SpecializationsRepository.GetByIdAsync(id);
            return specialization;
        }

        public async Task<bool> Update(Specialization specialization)
        {
            await _unitOfWork.SpecializationsRepository.UpdateAsync(specialization);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<Specialization> GetByName(string name)
        {
            return await _unitOfWork.SpecializationsRepository.GetBySpecializationName(name);
        }
    }
}