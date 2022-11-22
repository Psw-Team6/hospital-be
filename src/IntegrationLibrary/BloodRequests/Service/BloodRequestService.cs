using IntegrationLibrary.BloodRequests.Model;
using IntegrationLibrary.BloodRequests.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodRequests.Service
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly IBloodRequestRepository _bloodRequestRepository;

        public BloodRequestService(IBloodRequestRepository bloodRequestRepository)
        {
            _bloodRequestRepository = bloodRequestRepository;
        }
        public void Create(BloodRequest request)
        {
            _bloodRequestRepository.Create(request);
        }

        public void Delete(BloodRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BloodRequest> GetAll()
        {
            return _bloodRequestRepository.GetAll();
        }

        public BloodRequest GetFirst()
        {
            return _bloodRequestRepository.GetFirst();
        }

        public BloodRequest GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(BloodRequest request)
        {
            _bloodRequestRepository.Update(request);
        }

        public IEnumerable<BloodRequest> GetAllOnPending()
        {
            return _bloodRequestRepository.GetAllOnPending();
        }
    }
}
