using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Appointments.DomainEvents
{
    public class SpecializationViewedEvent : DomainEvent<EventStoreSchedulingAppointmentType>
    {
        public SpecializationViewedEvent(Guid aggregateId) : base(aggregateId)
        {
            
        }
    }
}