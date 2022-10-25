using System.Collections.Generic;
using AutoMapper;
using HospitalAPI.Dtos;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.sharedModel;

namespace HospitalAPI.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
            {
                CreateMap<SpecializationDto, Specialization>();
                CreateMap<Specialization, SpecializationDto>();
                CreateMap<Doctor, DoctorResponse>();
               // CreateMap<IEnumerable<DoctorResponse>, IEnumerable<Doctor>>();
                CreateMap<DoctorRequest,Doctor>();
                CreateMap<RoomResponse,Room>();
                CreateMap<RoomRequest,Room>();
                CreateMap<AddressResponse,Address>();
                CreateMap<Address,AddressResponse>();
                CreateMap<Room,RoomResponse>();
            }
    }
    
}