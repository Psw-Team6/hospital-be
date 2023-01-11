using System;
using System.Threading.Tasks;

namespace HospitalLibrary.Common.EventSourcing
{
    public interface IEventStoreService
    {
        Task<int> GetVersion(Guid aggregateId);
        Task<int> GetSequence();
    }
}