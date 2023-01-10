using System;
using System.ComponentModel;

namespace HospitalLibrary.Common.EventSourcing
{
    public abstract class EventStore<T,S>
    {
        public Guid Id { get; set; }
        public S Data { get; set; }
        public int Version { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        
        [Description("ignore")]
        public int Sequence { get; set; }

        public string Name { get; set; }
        public T Aggregate { get; set; }
        public Guid AggregateId { get; set; }
        
    }
}