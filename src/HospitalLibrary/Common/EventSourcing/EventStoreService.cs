using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Examinations.Model;

namespace HospitalLibrary.Common.EventSourcing
{
    public interface IEventStoreService
    {
        Task<int> GetVersion(Guid aggregateId);
        Task<int> GetSequence();

        Task CreateEventStore(Examination examination, List<DomainEvent<EventStoreExaminationType>> events);
    }
}