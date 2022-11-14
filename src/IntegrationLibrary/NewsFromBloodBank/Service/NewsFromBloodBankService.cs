using IntegrationLibrary.NewsFromBloodBank.Repository;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.NewsFromBloodBank.Service
{
    public class NewsFromBloodBankService : INewsFromBloodBankService
    {
        private readonly INewsFromBloodBankRepository _newsFromBloodBankRepository;

        public NewsFromBloodBankService(INewsFromBloodBankRepository bloodBankRepository)
        {
            _newsFromBloodBankRepository = bloodBankRepository;
        }

        public void Create(Model.NewsFromBloodBank news)
        {
            _newsFromBloodBankRepository.Create(news);
        }

        public void Delete(Model.NewsFromBloodBank news)
        {
            _newsFromBloodBankRepository.Delete(news);
        }

        public IEnumerable<Model.NewsFromBloodBank> GetAll()
        {
            return _newsFromBloodBankRepository.GetAll();
        }

        public Model.NewsFromBloodBank GetById(Guid id)
        {
            return _newsFromBloodBankRepository.GetById(id);
        }

        public Model.NewsFromBloodBank GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.NewsFromBloodBank news)
        {
            _newsFromBloodBankRepository.Update(news);
        }

    }
}
