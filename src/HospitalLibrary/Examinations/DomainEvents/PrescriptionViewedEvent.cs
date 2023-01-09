using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Examinations.DomainEvents
{
    public class PrescriptionViewedEvent : DomainEvent
    {
        public PrescriptionViewedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}