using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Appointments.DomainEvents
{
    public class FreeTermsViewedEvent : DomainEvent<EventStoreSchedulingAppointmentType>
    {
        public FreeTermsViewedEvent(Guid aggregateId) : base(aggregateId)
        {
            
        }
    }
}