using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;

namespace HospitalLibrary.EquipmentMovement
{
    public abstract class ScheduledProcessorHospital : ScopedProcessorHospital
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;

        protected abstract string Schedule { get; }

        public ScheduledProcessorHospital(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;

                if (now > _nextRun)
                {
                    await Process();

                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }

                await Task.Delay(5000, stoppingToken); // 5 seconds delay

            } while (!stoppingToken.IsCancellationRequested);
        }
    }
}