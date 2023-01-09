using System;

namespace HospitalLibrary.Common.EventSourcing
{
    public abstract class DomainEvent
    {
        protected DomainEvent(Guid aggregateId)
        {
            Id = aggregateId;
        }
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; set; }

    }
}