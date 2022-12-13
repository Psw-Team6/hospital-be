using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.Repository
{
    public interface IBloodUnitAmountRepository
    {
        public void Create(Model.BloodUnitAmount bloodUnitAmount);
        public IEnumerable<Model.BloodUnitAmount> GetAll();
        public Model.BloodUnitAmount GetById(Guid id);
    }
}
