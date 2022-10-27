using IntegrationLibrary.BloodBank.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodBank.Service
{
    public class BloodBankService : IBloodBankService
    {
        private readonly IBloodBankRepository _bloodBankRepository;

        public BloodBankService(IBloodBankRepository bloodBankRepository) { 
            _bloodBankRepository = bloodBankRepository;
        }
        public void Create(BloodBank bloodBank)
        {
            _bloodBankRepository.Create(bloodBank);
        }

        public void Delete(BloodBank bloodBank)
        {
            _bloodBankRepository.Delete(bloodBank);
        }

        public IEnumerable<BloodBank> GetAll()
        {
            return _bloodBankRepository.GetAll();
        }

        public BloodBank GetById(Guid id)
        {
            return _bloodBankRepository.GetById(id);
        }

        public void Update(BloodBank bloodBank)
        {
            _bloodBankRepository.Update(bloodBank);
        }
    }
}
