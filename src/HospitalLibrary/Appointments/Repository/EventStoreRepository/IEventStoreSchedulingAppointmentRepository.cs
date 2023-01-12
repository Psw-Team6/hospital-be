using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.DomainEvents;
using HospitalLibrary.Common;

namespace HospitalLibrary.Appointments.Repository.EventStoreRepository
{
    public interface IEventStoreSchedulingAppointmentRepository : IGenericRepository<EventStoreSchedulingAppointment>
    {
        Task<int> GetVersionCount(Guid aggregateId);
        Task<int> GetSequenceCount();
    }
}