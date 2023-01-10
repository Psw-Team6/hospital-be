using System;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.EventStores;

namespace HospitalLibrary.Examinations.DomainEvents
{
    public class PrescriptionViewedEvent :  DomainEvent<EventStoreExaminationType>
    {
        public PrescriptionViewedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}