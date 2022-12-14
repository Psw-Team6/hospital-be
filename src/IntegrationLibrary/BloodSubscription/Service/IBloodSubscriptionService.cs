using IntegrationLibrary.BloodSubscription.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodSubscription.Service
{
    public interface IBloodSubscriptionService
    {
        IEnumerable<MounthlyBloodSubscription> GetAll();
        MounthlyBloodSubscription GetById(Guid id);
        void Create(MounthlyBloodSubscription sub);
        void Update(MounthlyBloodSubscription sub);
        void Delete(MounthlyBloodSubscription sub);
    }
}
