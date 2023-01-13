using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Appointments.DomainEvents
{
    public class SchedulingAppointmentFinishedEvent : DomainEvent<EventStoreSchedulingAppointmentType>
    {
        public SchedulingAppointmentFinishedEvent(Guid aggregateId) : base(aggregateId)
        {

        }
    }
}