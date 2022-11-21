using System;
using System.Collections.Generic;

namespace IntegrationLibrary.NewsFromBloodBank.Repository
{
    public interface INewsFromBloodBankRepository
    {
        IEnumerable<NewsFromBloodBank.Model.NewsFromBloodBank> GetAll();
        NewsFromBloodBank.Model.NewsFromBloodBank GetById(Guid id);
        NewsFromBloodBank.Model.NewsFromBloodBank GetByName(String name);
        void Create(NewsFromBloodBank.Model.NewsFromBloodBank bloodBank);
        void Update(NewsFromBloodBank.Model.NewsFromBloodBank bloodBank);
        void Delete(NewsFromBloodBank.Model.NewsFromBloodBank bloodBank);
        public IEnumerable<NewsFromBloodBank.Model.NewsFromBloodBank> GetAllOnHold();
    }
}
