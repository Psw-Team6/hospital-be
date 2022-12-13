using IntegrationLibrary.BloodSubscription.Model;
using IntegrationLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodSubscription.Repository
{
    public class MounthlyBloodSubscriptionRepository : IMounthlyBloodSubscriptionRepository
    {
        private readonly IntegrationDbContext _context;
        public MounthlyBloodSubscriptionRepository(IntegrationDbContext context)
        {
            _context = context;
        }
        public void Create(MounthlyBloodSubscription sub)
        {
            _context.BloodSubscriptions.Add(sub);
            _context.SaveChanges();
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
