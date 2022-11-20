using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;

namespace IntegrationAPI.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<BloodBankRequest, BloodBank>();
            CreateMap<BloodBankName, BloodBank>();
        }



    }
}
