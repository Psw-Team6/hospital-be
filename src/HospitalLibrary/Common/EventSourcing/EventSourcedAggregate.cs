using System;
using System.Collections.Generic;

namespace HospitalLibrary.Common.EventSourcing
{
    public abstract class EventSourcedAggregate : Entity<Guid> 
    {
        public List<DomainEvent> Changes { get; private set; }

        protected EventSourcedAggregate(Guid id) : base(id)
        {
            Changes = new List<DomainEvent>();
        }
        public abstract void Apply(DomainEvent changes);

    }
}