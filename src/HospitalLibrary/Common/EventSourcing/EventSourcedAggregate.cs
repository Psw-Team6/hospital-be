using System;
using System.Collections.Generic;

namespace HospitalLibrary.Common.EventSourcing
{
    public abstract class EventSourcedAggregate<T> : Entity<Guid> 
    {
        protected EventSourcedAggregate(Guid id) : base(id)
        {
        }

        public List<DomainEvent<T>> Changes { get; private set; }

        public EventSourcedAggregate()
        {
            Changes = new List<DomainEvent<T>>();
        }

        public abstract void Apply(DomainEvent<T> @event);
    }
}