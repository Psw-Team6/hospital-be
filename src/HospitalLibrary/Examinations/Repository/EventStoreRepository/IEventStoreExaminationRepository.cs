using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Examinations.EventStores;

namespace HospitalLibrary.Examinations.Repository.EventStoreRepository
{
    public interface IEventStoreExaminationRepository : IGenericRepository<EventStoreExamination>
    {
        Task<int> GetVersionCount(Guid aggregateId);
        Task<int> GetSequenceCount();
        Task<int> GetEventCountByAggregate(Guid aggregateId);
        Task<List<EventStoreExamination>> GetEventsByAggregate(Guid aggregateId);
        Task<int> GetAverageViewForType(EventStoreExaminationType type);
        Task<List<EventStoreExamination>> GetEventsWithoutType(EventStoreExaminationType type, Guid aggregateId);
        Task<List<EventStoreExamination>> GetEventsBySpecialization(Guid specializationId);
        
    }
}