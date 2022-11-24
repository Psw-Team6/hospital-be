using System;
using System.Threading.Tasks;
using HospitalLibrary.EquipmentMovement.Service;
using IntegrationAPI.ScheduleTask.Service;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalLibrary.EquipmentMovement
{
    public class CheckIfAppointmentIsDone: ScheduledProcessorHospital
    {
        public CheckIfAppointmentIsDone(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }

        protected override string Schedule => "*/1 * * * *"; // every 1 min 

        public override async Task ProcessInScope(IServiceProvider scopeServiceProvider)
        {
            IEquipmentMovementAppointmentService reportSenderService = scopeServiceProvider.GetRequiredService<IEquipmentMovementAppointmentService>();
            reportSenderService.CheckAllAppointmentTimes();
            Console.WriteLine("-------------------------");
            Console.WriteLine("PikulaTask1 : " + DateTime.Now.ToString());

            await Task.Run(() => {
                return Task.CompletedTask;
            });
        }
    }
}