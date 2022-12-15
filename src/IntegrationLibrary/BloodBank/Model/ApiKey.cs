using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodBank.Model
{
    public class ApiKey
    {
        public String Value { get; private set; }
        public ApiKey()
        {
            Value = GenerateApiKey();
            Validate();
        }

        private void Validate()
        {

        }

        private String GenerateApiKey()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            return Convert.ToBase64String(key);

        }

        public override bool Equals(object obj)
        {
            return Value.Equals(((ApiKey)obj).Value);
        }
    }
}
