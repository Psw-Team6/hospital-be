using IntegrationLibrary.BloodRequests.Model;
using IntegrationLibrary.Settings;
using IntegrationLibrary.Tender.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.Repository
{
    public class TenderRepository : ITenderRepository
    {
        private readonly IntegrationDbContext _context;
        protected readonly DbSet<Model.Tender> DbSet;

        public TenderRepository(IntegrationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<Model.Tender>();
        }

        public void Create (Model.Tender tender)
        {
            _context.Tenders.Add(tender);
            _context.SaveChanges();
        }
        public void Delete(Model.Tender tender)
        {
            _context.Tenders.Remove(tender);
            _context.SaveChanges();
        }

        public async Task<List<Model.Tender>> GetAll()
        {
            // return _context.Tenders.ToList();
            return  await DbSet.Include(d => d.BloodUnitAmount).ToListAsync();
        }

        public List<Model.Tender> GetAllTenders()
        {
            return _context.Tenders.ToList();
        }

        public Model.Tender GetById(Guid id)
        {
            return _context.Tenders.Find(id);
        }

        public void Update(Model.Tender tender)
        {
            _context.Entry(tender).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }


    }


}
