using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Settings;

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
    }
}