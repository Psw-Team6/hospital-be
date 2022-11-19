using AutoMapper;
using HospitalLibrary.BloodConsumptions.Model;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.PDFReports.Model;

namespace IntegrationAPI.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<BloodBankRequest, BloodBank>();
            CreateMap<BloodConsumption, BloodConsumptionPDFReport>();
            CreateMap<BloodConsumptionPDFReport, BloodConsumption>();
        }
            
    }
}
