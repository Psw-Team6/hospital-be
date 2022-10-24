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

        public async Task<SpecializationDto> Create(SpecializationDto specializationDto)
        {
            var convert = _mapper.Map<Specialization>(specializationDto);
            var spec =await _unitOfWork.SpecializationsRepository.CreateAsync(convert);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<SpecializationDto>(spec);
        }

        public async  Task<SpecializationDto> GetById(Guid id)
        {
            var spec = await _unitOfWork.SpecializationsRepository.GetByIdAsync(id);
            return _mapper.Map<SpecializationDto>(spec);
        }
    }
}