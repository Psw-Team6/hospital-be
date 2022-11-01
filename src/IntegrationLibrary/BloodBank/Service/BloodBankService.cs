using IntegrationLibrary.BloodBank.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            bloodBank.Password = String.Concat(bloodBank.Name.Where(c => !Char.IsWhiteSpace(c))) + "12345";
            bloodBank.ApiKey = GenerateApiKey();
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

        private String GenerateApiKey() {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            return Convert.ToBase64String(key);
            
        }
    }
}
