
using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Examinations.DomainEvents
{
    public class ExaminationStartedEvent : DomainEvent
    {
        public ExaminationStartedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}