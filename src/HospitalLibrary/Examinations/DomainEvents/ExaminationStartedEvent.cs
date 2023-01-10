
using System;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.EventStores;

namespace HospitalLibrary.Examinations.DomainEvents
{
    public class ExaminationStartedEvent :  DomainEvent<EventStoreExaminationType>
    {
        public ExaminationStartedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}