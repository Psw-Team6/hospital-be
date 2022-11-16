using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.NewsFromBloodBank.Service
{
    public interface INewsFromBloodBankService
    {
        IEnumerable<Model.NewsFromBloodBank> GetAll();
        Model.NewsFromBloodBank GetById(Guid id);
        Model.NewsFromBloodBank GetByName(String name);
        void Create(Model.NewsFromBloodBank news);
        void Update(Model.NewsFromBloodBank news);
        void Delete(Model.NewsFromBloodBank news);
        public IEnumerable<Model.NewsFromBloodBank> GetAllOnHold();
    }
}
