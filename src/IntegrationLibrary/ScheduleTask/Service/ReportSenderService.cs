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
                    if (allDatesForSend[i].SendPeriod.Equals("EVERY_TWO_MINUT"))
                    {
                        DateTime currentTime = DateTime.Now;
                        allDatesForSend[i].NextDateForSending = currentTime.AddMinutes(2);
                        Console.WriteLine(allDatesForSend[i].NextDateForSending);
                    }
                    else
                    {
                        allDatesForSend[i].NextDateForSending = allDatesForSend[i].NextDateForSending.AddDays(calculateDate.DefinePeriodForSendingReports(allDatesForSend[i].SendPeriod));
                    }
                    _configureGenerateAndSendRepository.Update(allDatesForSend[i]);
                    Console.WriteLine("Send message: "+ allDatesForSend[i].NextDateForSending);
                   
                }
            }
        }

       
    }
}
