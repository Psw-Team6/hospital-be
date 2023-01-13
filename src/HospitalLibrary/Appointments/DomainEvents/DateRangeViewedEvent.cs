using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Appointments.DomainEvents
{
    public class DateRangeViewedEvent : DomainEvent<EventStoreSchedulingAppointmentType>
    {
        public DateRangeViewedEvent(Guid aggregateId) : base(aggregateId)
        {
            
        }
    }
}