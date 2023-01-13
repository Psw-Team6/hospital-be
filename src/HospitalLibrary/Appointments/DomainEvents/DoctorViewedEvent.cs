using System;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Appointments.DomainEvents
{
    public class DoctorViewedEvent : DomainEvent<EventStoreSchedulingAppointmentType>
    {
        public DoctorViewedEvent(Guid aggregateId) : base(aggregateId)
        {
            
        }
    }
}