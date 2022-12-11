using AutoMapper;

namespace IntegrationAPI.Mapper
{
    public class MapperProvider
    {
        public MapperProvider()
        {

        }
        public MapperConfiguration GetMapperConfig()
        {
            var mce = new MapperConfigurationExpression();
            mce.AddProfile<MappingProfile>();
            var mc = new MapperConfiguration(mce);
            return mc;
        }

    }
}