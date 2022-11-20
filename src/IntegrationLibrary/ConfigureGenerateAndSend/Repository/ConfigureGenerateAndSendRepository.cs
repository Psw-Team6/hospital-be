using IntegrationLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.ConfigureGenerateAndSend.Repository
{
   public class ConfigureGenerateAndSendRepository: IConfigureGenerateAndSendRepository
    {
        private readonly IntegrationDbContext _context;

        public ConfigureGenerateAndSendRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public void Create(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configureGenerateAndSend)
        {
            _context.ConfigureGenerateAndSend.Add(configureGenerateAndSend);
            _context.SaveChanges();
        }
    }
}
