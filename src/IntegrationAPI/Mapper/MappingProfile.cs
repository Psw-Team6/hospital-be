using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationAPI.Dtos.Response;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.PDFReports.Model;

namespace IntegrationAPI.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<BloodBankRequest, BloodBank>();

        }
            
    }
}
