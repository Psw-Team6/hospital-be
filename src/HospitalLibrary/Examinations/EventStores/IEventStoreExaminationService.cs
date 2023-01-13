using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.Model;

namespace HospitalLibrary.Examinations.EventStores
{
    public interface IEventStoreExaminationService
    {
        Task<int> GetVersion(Guid aggregateId);
        Task<int> GetSequence();

        Task CreateEventStore(Examination examination, List<DomainEvent<EventStoreExaminationType>> events);   
    }
}