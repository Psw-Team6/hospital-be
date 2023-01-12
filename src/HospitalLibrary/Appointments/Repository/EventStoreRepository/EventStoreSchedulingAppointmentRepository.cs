using System;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.DomainEvents;
using HospitalLibrary.Common;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Appointments.Repository.EventStoreRepository
{
    public class EventStoreSchedulingAppointmentRepository : GenericRepository<EventStoreSchedulingAppointment>, IEventStoreSchedulingAppointmentRepository
    {

        public EventStoreSchedulingAppointmentRepository(HospitalDbContext dbContext) : base(dbContext)
        {
            
        }
        
        public Task<int> GetVersionCount(Guid aggregateId)
        {
            return  Task.FromResult(DbSet.Count(x => x.AggregateId == aggregateId));
        }

        public Task<int> GetSequenceCount()
        {
            return  Task.FromResult(DbSet.Count());
        }
        
    }
}