using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;

namespace IntegrationLibrary.ConfigureGenerateAndSend.Repository
{
    public interface IConfigureGenerateAndSendRepository
    {
        void Create(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configureGenerateAndSend);

        IEnumerable<ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend> GetAll();

        void Update(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configureGenerateAndSend);
    }
}
