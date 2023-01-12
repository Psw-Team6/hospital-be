using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Appointments.DomainEvents
{
    public class SchedulingAppointmentStartedEvent : DomainEvent<EventStoreSchedulingAppointmentType>
    {
        public SchedulingAppointmentStartedEvent(Guid aggregateId) : base(aggregateId)
        {
            
        }
    }
}