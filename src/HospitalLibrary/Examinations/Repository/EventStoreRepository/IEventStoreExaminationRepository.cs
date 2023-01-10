using System;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.EventStores;

namespace HospitalLibrary.Examinations.Repository.EventStoreRepository
{
    public interface IEventStoreExaminationRepository : IGenericRepository<EventStoreExamination>
    {
        Task<int> GetVersionCount(Guid aggregateId);
        Task<int> GetSequenceCount();
    }
}