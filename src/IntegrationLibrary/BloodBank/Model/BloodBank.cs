using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodBank
{
    public class BloodBank
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String ServerAddress { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String ApiKey { get; set; }
    }
}
