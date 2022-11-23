using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodBank.Repository
{
    public class BloodBankRepository : IBloodBankRepository
    {
        private readonly IntegrationDbContext _context;

        public BloodBankRepository(IntegrationDbContext context) { 
            _context = context;
        }
        public void Create(BloodBank bloodBank)
        {
            _context.BloodBanks.Add(bloodBank);
            _context.SaveChanges();
        }

        public void Delete(BloodBank bloodBank)
        {
            _context.BloodBanks.Remove(bloodBank);
            _context.SaveChanges();
        }

        public BloodBank Authenticate(string username, string password)
        {
            return _context.BloodBanks
                .FirstOrDefault(bank => bank.Name == username && bank.Password == password);
        }

        public IEnumerable<BloodBank> GetAll()
        {
            return _context.BloodBanks.ToList();
        }

        public BloodBank GetById(Guid id)
        {
            return _context.BloodBanks.Find(id);
        }
        public BloodBank GetByName(String name)
        {
            var pom = _context.BloodBanks.FromSqlRaw<BloodBank>("select * from public.\"BloodBanks\" where \"Name\" =" + "'" + name + "'");
            return pom.FirstOrDefault();
        }


        public void Update(BloodBank bloodBank)
        {
            _context.Entry(bloodBank).State = EntityState.Modified;

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
