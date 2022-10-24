using AutoMapper;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Common.mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
            {
                CreateMap<SpecializationDto, Specialization>();

                CreateMap<Specialization, SpecializationDto>();
            }
    }
    
}