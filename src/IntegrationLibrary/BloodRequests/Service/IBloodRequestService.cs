using IntegrationLibrary.BloodRequests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodRequests.Service
{
    public interface IBloodRequestService
    {
        IEnumerable<BloodRequest> GetAll();
        IEnumerable<BloodRequest> GetAllOnPending();
        BloodRequest GetById(Guid id);
        void Create(BloodRequest request);
        void Update(BloodRequest request);
        void Delete(BloodRequest request);
        BloodRequest GetFirst();
    }
}
