using IntegrationLibrary.BloodRequests.Model;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodRequests.Repository
{
    public class BloodRequestRepository : IBloodRequestRepository
    {
        private readonly IntegrationDbContext _context;

        public BloodRequestRepository(IntegrationDbContext context)
        {
            _context = context;
        }
        public void Create(BloodRequest request)
        {
            _context.BloodRequests.Add(request);
            _context.SaveChanges();
        }

        public void Delete(BloodRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BloodRequest> GetAll()
        {
            return _context.BloodRequests.ToList();
        }

        public BloodRequest GetFirst()
        {
            return _context.BloodRequests.ToList().First();
        }

        public BloodRequest GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(BloodRequest request)
        {
            _context.Entry(request).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public IEnumerable<BloodRequest> GetAllOnPending()
        {
            List<BloodRequest> all = (List<BloodRequest>)GetAll();
            List<BloodRequest> allOnPending = new List<BloodRequest>();

            foreach (BloodRequest req in all)
            {
                if (req.Status == Status.PENDING)
                {
                    allOnPending.Add(req);
                }
            }

            return allOnPending;
        }
    }
}
