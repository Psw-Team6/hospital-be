using IntegrationLibrary.BloodBank.Repository;
using IntegrationLibrary.SendMail;
using IntegrationLibrary.SendMail.Services;
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
        private readonly IEmailService _emailService;

        public BloodBankService(IBloodBankRepository bloodBankRepository, IEmailService emailService)
        {
            _bloodBankRepository = bloodBankRepository;
            _emailService = emailService;
        }
        public void Create(BloodBank bloodBank)
        {
            bloodBank.Password = GenerateDummyPassword();
            bloodBank.ApiKey = GenerateApiKey();
            String user = bloodBank.Name;
            String link = "Dear " + user + ",\n Click on link " + "<a href=\"http://localhost:4200/bloodBank/changePassword\">Change password</a>"
                + " and change your initial password.\n\n Your username is <strong>" + user + "</strong> and initial password is <strong>" + bloodBank.Password + "</strong>.";
            String mail = bloodBank.Email;
            _emailService.SendEmail(new Email(mail, "PSW-hospital", "TEKST", link));
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

        private String GenerateDummyPassword()
        {
            var key = new byte[8];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            return Convert.ToBase64String(key);

        }
    }
}
