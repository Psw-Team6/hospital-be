using IntegrationLibrary.Enums;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.NewsFromBloodBank.Repository
{
    public class NewsFromBloodBankRepository : INewsFromBloodBankRepository
    {
        private readonly IntegrationDbContext _context;
        public NewsFromBloodBankRepository(IntegrationDbContext context)
        {
            _context = context;
        }
        public void Create(Model.NewsFromBloodBank news)
        {
            _context.NewsFromBloodBank.Add(news);
            _context.SaveChanges();
        }

        public void Delete(Model.NewsFromBloodBank news)
        {
            _context.NewsFromBloodBank.Remove(news);
            _context.SaveChanges();
        }

        public IEnumerable<Model.NewsFromBloodBank> GetAll()
        {
            return _context.NewsFromBloodBank.ToList();
        }

        public Model.NewsFromBloodBank GetById(Guid id)
        {
            return _context.NewsFromBloodBank.Find(id);
        }

        public Model.NewsFromBloodBank GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.NewsFromBloodBank news)
        {
            _context.Entry(news).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public IEnumerable<NewsFromBloodBank.Model.NewsFromBloodBank> GetAllOnHold()
        {
            return _context.NewsFromBloodBank.Where(newFromBloodBank => newFromBloodBank.newsStatus == 0);
        }

        public IEnumerable<Model.NewsFromBloodBank> GetAllForBloodSubscription()
        {
            return _context.NewsFromBloodBank.Where(newFromBloodBank => newFromBloodBank.newsStatus == NewsFromHospitalStatus.BLOOD_SUBSCRIPTION);
        }
    }
}
