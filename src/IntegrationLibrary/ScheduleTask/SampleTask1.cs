using IntegrationLibrary.BackgroundService;
using IntegrationLibrary.ScheduleTask.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.ScheduleTask
{
    public class SampleTask1 : ScheduledProcessor
    {
        public SampleTask1(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }

        protected override string Schedule => "*/1 * * * *"; // every 1 min 

        public override async Task ProcessInScope(IServiceProvider scopeServiceProvider)
        {
            IReportSenderService reportSenderService = scopeServiceProvider.GetRequiredService<IReportSenderService>();
            reportSenderService.IsTimeForSending();
            Console.WriteLine("-------------------------");
            Console.WriteLine("SampleTask1 : " + DateTime.Now.ToString());

            await Task.Run(() => {
                return Task.CompletedTask;
            });
        }
    }
}
