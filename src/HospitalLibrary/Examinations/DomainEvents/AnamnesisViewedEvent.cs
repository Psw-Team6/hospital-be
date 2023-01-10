using System;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.EventStores;

namespace HospitalLibrary.Examinations.DomainEvents
{ 
    public class AnamnesisViewedEvent : DomainEvent<EventStoreExaminationType>
    {
        public AnamnesisViewedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}