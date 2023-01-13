using IntegrationLibrary.BloodBank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Unit
{
    public class ApiKeyTest
    {
        [Fact]
        public async Task checkGeneratingApiKey()
        {
            ApiKey api = new ApiKey();
            Assert.True(api.Value != null);
        }
    }
}
