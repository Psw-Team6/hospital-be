using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Examinations.DomainEvents
{
    public class ViewExaminationInfoEvent : DomainEvent
    {
        public ViewExaminationInfoEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}