using System;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.EventStores;

namespace HospitalLibrary.Examinations.DomainEvents
{
    public class SymptomsViewedEvent :  DomainEvent<EventStoreExaminationType>
    {
        public SymptomsViewedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
        public SymptomsViewedEvent(DateTime createdAt, EventStoreExaminationType @event) :  base(createdAt,@event)
        {
        }
    }
}