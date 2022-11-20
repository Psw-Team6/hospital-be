using System;

namespace IntegrationAPI.Dtos.Request
{
    public class ConfigureGenerateAndSendRequest
    {
        public string BloodBankName { get; set; }
        public string GenertePeriod { get; set; }
        public string SendPeriod { get; set; }
        public DateTime NextDateForSending { get; set; }


    }
}
