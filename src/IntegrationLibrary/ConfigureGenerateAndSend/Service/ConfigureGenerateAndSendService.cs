using IntegrationLibrary.ConfigureGenerateAndSend.Repository;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.ConfigureGenerateAndSend.Service
{
   public class ConfigureGenerateAndSendService: IConfigureGenerateAndSendService
    {
        private readonly IConfigureGenerateAndSendRepository _configureGenerateAndSendRepository;

        public ConfigureGenerateAndSendService(IConfigureGenerateAndSendRepository configureGenerateAndSendRepository)
        {
            _configureGenerateAndSendRepository = configureGenerateAndSendRepository;

        }
        public void Create(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configureGenerateAndSend)
        {
            _configureGenerateAndSendRepository.Create(configureGenerateAndSend);

        }

       
    }
}
