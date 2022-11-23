using IntegrationLibrary.BloodRequests.Model;
using System;
using System.Collections.Generic;


namespace IntegrationLibrary.BloodRequests.Repository
{
    public interface IBloodRequestRepository
    {
        IEnumerable<BloodRequest> GetAll();
        IEnumerable<BloodRequest> GetAllOnPending();
        IEnumerable<BloodRequest> GetAllReturned(string doctorUsername);

        BloodRequest GetById(Guid id);
        void Create(BloodRequest request);
        void Update(BloodRequest request);
        void Delete(BloodRequest request);
        BloodRequest GetFirst();

    }
}
