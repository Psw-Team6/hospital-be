using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.ConfigureGenerateAndSend.Model
{
    public class ConfigureGenerateAndSend
    {

        public Guid Id { get; set; }
        public string BloodBankName { get; set; }
        public string GeneratePeriod { get; set; }
        public string SendPeriod { get; set; }

        public DateTime NextDateForSending { get; set; }

        public ConfigureGenerateAndSend(string bloodBankName, string generatePeriod, string sendPeriod, DateTime nextDateForSending)
        {
            BloodBankName = bloodBankName;
            GeneratePeriod = generatePeriod;
            SendPeriod = sendPeriod;
            NextDateForSending = nextDateForSending;
        }
    }
}
