using IntegrationAPI.Controllers;
using IntegrationLibrary.BloodRequests.Service;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;
using IntegrationLibrary.ConfigureGenerateAndSend.Repository;
using IntegrationLibrary.ConfigureGenerateAndSend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAPI.ScheduleTask.Service
{
   public class ReportSenderService: IReportSenderService
    {
        private readonly IConfigureGenerateAndSendRepository _configureGenerateAndSendRepository;
        private readonly IBloodRequestService _bloodRequestService;
        private CalculateDate calculateDate = new CalculateDate();
        private readonly PDFReportController _PDFReportController;
        

        public ReportSenderService(IConfigureGenerateAndSendRepository configureGenerateAndSendRepository, PDFReportController pDFReportController, IBloodRequestService bloodRequestService)
        {
            _configureGenerateAndSendRepository = configureGenerateAndSendRepository;
            _PDFReportController = pDFReportController;
            _bloodRequestService = bloodRequestService;
        }

       public List<DateTime> GetAllDateForSend()
        {
            List<DateTime> allDates = new List<DateTime>();
            List<ConfigureGenerateAndSend> allConfiguration = (List<ConfigureGenerateAndSend>)_configureGenerateAndSendRepository.GetAll();
            for (int i=0; i< allConfiguration.Count; i++)
            {
                allDates.Add(allConfiguration[i].NextDateForSending);
            }
            return allDates;
        }
       
        

        public void TimeForSending()
        {
            List<ConfigureGenerateAndSend> allDatesForSend = (List<ConfigureGenerateAndSend>)_configureGenerateAndSendRepository.GetAll();
            for (int i=0; i< allDatesForSend.Count; i++)
            {
                if (DateTime.Compare(allDatesForSend[i].NextDateForSending, DateTime.Now)<0)
                {
                    CalculateNextSendPeriod(allDatesForSend[i]);

                    _PDFReportController.sendReport(allDatesForSend[i].BloodBankName, calculateDate.DefinePeriodForSendingReports(allDatesForSend[i].GeneratePeriod));
                    _configureGenerateAndSendRepository.Update(allDatesForSend[i]);
                    Console.WriteLine("Send message: "+ allDatesForSend[i].NextDateForSending +" to "+ allDatesForSend[i].BloodBankName);

                    _bloodRequestService.sendScheduledRequest();
                    Console.WriteLine("Request is sent!");
                }
            }
        }


        public void CalculateNextSendPeriod(ConfigureGenerateAndSend configuration) {

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
