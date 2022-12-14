using IntegrationAPI.BackgroundTaskService;
using IntegrationAPI.ScheduleTask.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAPI.ScheduleTask
{
    public class GenerateAndSendReportTask : ScheduledProcessor
    {
        public GenerateAndSendReportTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }

        protected override string Schedule => "*/1 * * * *"; // every 1 min 

        public override async Task ProcessInScope(IServiceProvider scopeServiceProvider)
        {
            IReportSenderService reportSenderService = scopeServiceProvider.GetRequiredService<IReportSenderService>();
            reportSenderService.TimeForSending();
            Console.WriteLine("-------------------------");
            Console.WriteLine("SampleTask1 : " + DateTime.Now.ToString());

            await Task.Run(() => {
            return Task.CompletedTask;
            });

        }

        


    }
}
