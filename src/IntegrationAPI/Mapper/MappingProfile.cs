using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;

namespace IntegrationAPI.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<BloodBankRequest, BloodBank>();
        }
            
    }
}
