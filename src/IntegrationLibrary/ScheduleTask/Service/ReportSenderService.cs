using IntegrationLibrary.ConfigureGenerateAndSend.Repository;
using IntegrationLibrary.ConfigureGenerateAndSend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.ScheduleTask.Service
{
   public class ReportSenderService: IReportSenderService
    {
        private readonly IConfigureGenerateAndSendRepository _configureGenerateAndSendRepository;
        private CalculateDate calculateDate = new CalculateDate();
        

        public ReportSenderService(IConfigureGenerateAndSendRepository configureGenerateAndSendRepository)
        {
            _configureGenerateAndSendRepository = configureGenerateAndSendRepository;
        }

       public List<DateTime> GetAllDateForSend()
        {
            List<DateTime> allDates = new List<DateTime>();
            List<ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend> allConfiguration = (List<ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend>)_configureGenerateAndSendRepository.GetAll();
            for (int i=0; i< allConfiguration.Count; i++)
            {
                allDates.Add(allConfiguration[i].NextDateForSending);
            }
            return allDates;
        }
       
        

        public void IsTimeForSending()
        {
            List<ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend> allDatesForSend = (List<ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend>)_configureGenerateAndSendRepository.GetAll();
            for (int i=0; i< allDatesForSend.Count; i++)
            {
                if (DateTime.Compare(allDatesForSend[i].NextDateForSending, DateTime.Now)<0)
                {
                    CalculateNextSendPeriod(allDatesForSend[i]);
                    _configureGenerateAndSendRepository.Update(allDatesForSend[i]);
                    Console.WriteLine("Send message: "+ allDatesForSend[i].NextDateForSending);

                }
            }
        }


        public void CalculateNextSendPeriod(ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configuration) {

            if (configuration.SendPeriod.Equals("EVERY_TWO_MINUT"))
            {
                configuration.NextDateForSending = DateTime.Now.AddMinutes(2);
            }
            else
            {
                configuration.NextDateForSending = configuration.NextDateForSending.AddDays(calculateDate.DefinePeriodForSendingReports(configuration.SendPeriod));
            }
        }

       
    }
}
