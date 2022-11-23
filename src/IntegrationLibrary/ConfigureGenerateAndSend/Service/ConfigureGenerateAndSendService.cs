using IntegrationLibrary.ConfigureGenerateAndSend.Repository;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IntegrationLibrary.ConfigureGenerateAndSend.Service
{
   public class ConfigureGenerateAndSendService: IConfigureGenerateAndSendService
    {
        private readonly IConfigureGenerateAndSendRepository _configureGenerateAndSendRepository;
        private CalculateDate calculateDate = new CalculateDate();


        public ConfigureGenerateAndSendService(IConfigureGenerateAndSendRepository configureGenerateAndSendRepository)
        {
            _configureGenerateAndSendRepository = configureGenerateAndSendRepository;

        }

        public ConfigureGenerateAndSendService()
        {
        }


        public IEnumerable<ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend> GetAll()
        {
            return _configureGenerateAndSendRepository.GetAll();
        }

        public void Create(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configureGenerateAndSend)
        {
            if (configureGenerateAndSend.SendPeriod.Equals("EVERY_TWO_MINUT"))
            {
                DateTime currentTime = DateTime.Now;
                configureGenerateAndSend.NextDateForSending = currentTime.AddMinutes(2);
            }
            else
            {
                configureGenerateAndSend.NextDateForSending = DateTime.Today.AddDays(calculateDate.DefinePeriodForSendingReports(configureGenerateAndSend.SendPeriod));
            }
            
            _configureGenerateAndSendRepository.Create(configureGenerateAndSend);

        }

       

        public void Update(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configureGenerateAndSend)
        {
            if (configureGenerateAndSend.SendPeriod.Equals("EVERY_TWO_MINUT"))
            {
                DateTime currentTime = DateTime.Now;
                configureGenerateAndSend.NextDateForSending = currentTime.AddMinutes(2);
                Console.WriteLine(configureGenerateAndSend.NextDateForSending);
            }
            else
            {
                configureGenerateAndSend.NextDateForSending = configureGenerateAndSend.NextDateForSending.AddDays(calculateDate.DefinePeriodForSendingReports(configureGenerateAndSend.SendPeriod));
            }
            _configureGenerateAndSendRepository.Update(configureGenerateAndSend);
        }

        public void Edit(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configureGenerateAndSend)
        {
                configureGenerateAndSend.NextDateForSending = DateTime.Now;
            _configureGenerateAndSendRepository.Edit(configureGenerateAndSend);

        }



        public void Delete(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configuration)
        {
            _configureGenerateAndSendRepository.Delete(configuration);
        }
    }
}
