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
        private readonly IMapper _mapper;

        public SpecializationsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}