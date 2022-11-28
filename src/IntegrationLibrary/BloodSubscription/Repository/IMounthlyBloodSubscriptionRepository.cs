using IntegrationLibrary.BloodSubscription.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodSubscription.Repository
{
    public interface IMounthlyBloodSubscriptionRepository
    {
        IEnumerable<MounthlyBloodSubscription> GetAll();
        MounthlyBloodSubscription GetById(Guid id);
        void Create(MounthlyBloodSubscription sup);
        void Update(MounthlyBloodSubscription sup);
        void Delete(MounthlyBloodSubscription sup);
    }
}
