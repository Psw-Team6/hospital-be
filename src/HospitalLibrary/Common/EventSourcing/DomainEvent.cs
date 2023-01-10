using System;

namespace HospitalLibrary.Common.EventSourcing
{
    public  class DomainEvent<T>
    {
        protected DomainEvent() { }
        protected DomainEvent(Guid aggregateId)
        {
            Id = aggregateId;
        }
        public Guid Id { get; private set; }
        public T Event { get; private set; }
        public DateTime CreatedAt { get; private set; }
        

    }
}