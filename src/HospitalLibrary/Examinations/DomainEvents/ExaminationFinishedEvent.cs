using System;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.EventStores;

namespace HospitalLibrary.Examinations.DomainEvents
{
    public class ExaminationFinishedEvent :  DomainEvent<EventStoreExaminationType>
    {
        public ExaminationFinishedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}