using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Examinations.Repository.EventStoreRepository
{
    public class EventStoreExaminationRepository :GenericRepository<EventStoreExamination>,IEventStoreExaminationRepository
    {
        public EventStoreExaminationRepository(HospitalDbContext dbContext) : base(dbContext)
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

        public async Task<List<EventStoreExamination>> GetEventsByAggregate(Guid aggregateId)
        {
            var events = await DbSet.Where(x => x.AggregateId == aggregateId)
            .ToListAsync();
            return events;
        }

        public  Task<int> GetAverageViewForType(EventStoreExaminationType type)
        {
            return Task.FromResult(DbSet.Count(@event => @event.Data == EventStoreExaminationType.SYMPTOMS_VIEWED));
        }

        public async Task<List<EventStoreExamination>> GetEventsWithoutType(EventStoreExaminationType type, Guid aggregateId)
        {
            return await DbSet.Where(x => x.AggregateId == aggregateId && x.Data == type)
                .ToListAsync();
        }
    }
}