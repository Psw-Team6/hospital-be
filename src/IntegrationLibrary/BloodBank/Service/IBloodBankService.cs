using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodBank.Service
{
    public interface IBloodBankService
    {
        IEnumerable<BloodBank> GetAll();
        BloodBank GetById(Guid id);
        void Create(BloodBank bloodBank);
        void Update(BloodBank bloodBank);
        void Delete(BloodBank bloodBank);

    }
}
