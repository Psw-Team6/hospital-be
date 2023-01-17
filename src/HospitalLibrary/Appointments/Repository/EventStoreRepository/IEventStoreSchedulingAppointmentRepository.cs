using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.DomainEvents;
using HospitalLibrary.Common;

namespace HospitalLibrary.Appointments.Repository.EventStoreRepository
{
    public interface IEventStoreSchedulingAppointmentRepository : IGenericRepository<EventStoreSchedulingAppointment>
    {
        Task<int> GetVersionCount(Guid aggregateId);
        Task<int> GetSequenceCount();
        Task<int> GetEventCountByAggregate(Guid aggregateId);
        Task<List<EventStoreSchedulingAppointment>> GetEventsByAggregate(Guid aggregateId);
        Task<int> GetAverageViewForType(EventStoreSchedulingAppointmentType type);
        Task<List<EventStoreSchedulingAppointment>> GetEventsWithoutType(EventStoreSchedulingAppointmentType type, Guid aggregateId);
    }
}