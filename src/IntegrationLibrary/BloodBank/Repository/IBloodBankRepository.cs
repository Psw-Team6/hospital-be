using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodBank.Repository
{
    public interface IBloodBankRepository
    {
        IEnumerable<BloodBank> GetAll();
        BloodBank GetById(Guid id);
        BloodBank GetByName(String name);
        BloodBank GetByAPIKey(String APIKey);
        void Create(BloodBank bloodBank);
        void Update(BloodBank bloodBank);
        void Delete(BloodBank bloodBank); 
        BloodBank Authenticate(string username, string password);
    }
}
