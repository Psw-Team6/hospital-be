using IntegrationLibrary.BloodSubscription.Model;
using IntegrationLibrary.BloodSubscription.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodSubscription.Service
{
    public class BloodSubscriptionService : IBloodSubscriptionService
    {
        private readonly IMounthlyBloodSubscriptionRepository _supRepo;

        public BloodSubscriptionService(IMounthlyBloodSubscriptionRepository repo)
        {
            _supRepo = repo;
        }
        public void Create(MounthlyBloodSubscription sub)
        {
            _supRepo.Create(sub);
        }

        public void Delete(MounthlyBloodSubscription sub)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MounthlyBloodSubscription> GetAll()
        {
            throw new NotImplementedException();
        }

        public MounthlyBloodSubscription GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(MounthlyBloodSubscription sub)
        {
            throw new NotImplementedException();
        }
    }
}
