using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Examinations.DomainEvents
{ 
    public class AnamnesisViewedEvent : DomainEvent
    {
        public AnamnesisViewedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}