using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
namespace HospitalLibrary.EquipmentMovement
{
    public abstract class ScopedProcessorHospital: BackgroundServiceHospital
    {
        
        private IServiceScopeFactory _serviceScopeFactory;

        public ScopedProcessorHospital(IServiceScopeFactory serviceScopeFactory) : base()
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task Process()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                await ProcessInScope(scope.ServiceProvider);
            }
        }

        public abstract Task ProcessInScope(IServiceProvider scopeServiceProvider);
    }
}