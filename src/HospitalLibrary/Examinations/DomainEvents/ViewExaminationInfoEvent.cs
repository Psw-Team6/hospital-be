using System;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.EventStores;

namespace HospitalLibrary.Examinations.DomainEvents
{
    public class ViewExaminationInfoEvent :  DomainEvent<EventStoreExaminationType>
    {
        public ViewExaminationInfoEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}