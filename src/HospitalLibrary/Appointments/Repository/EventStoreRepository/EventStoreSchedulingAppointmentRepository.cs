using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        
        public Task<int> GetEventCountByAggregate(Guid aggregateId)
        {
            return Task.FromResult(DbSet
                .Count(x => x.AggregateId == aggregateId));
        }

        public async Task<List<EventStoreSchedulingAppointment>> GetEventsByAggregate(Guid aggregateId)
        {
            var events = await DbSet.Where(x => x.AggregateId == aggregateId)
                .ToListAsync();
            return events;
        }

        public  Task<int> GetAverageViewForType(EventStoreSchedulingAppointmentType type)
        {
            return Task.FromResult(DbSet.Count(@event => @event.Data == type));
        }

        public async Task<List<EventStoreSchedulingAppointment>> GetEventsWithoutType(EventStoreSchedulingAppointmentType type, Guid aggregateId)
        {
            return await DbSet.Where(x => x.AggregateId == aggregateId && x.Data == type)
                .ToListAsync();
        }
        
    }
}