using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;

namespace IntegrationLibrary.ConfigureGenerateAndSend.Service
{
   public interface IConfigureGenerateAndSendService
    {
        void Create(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configureGenerateAndSend);
        public void Update(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configureGenerateAndSend);
    }
}
