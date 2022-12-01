using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.Repository
{
    public interface ITenderRepository
    {
        public void Create(Model.Tender tender);
        public void Delete(Model.Tender tender);
        public IEnumerable<Model.Tender> GetAll();
        public void Update(Model.Tender tender);
        public Model.Tender GetById(Guid id);


    }
}
