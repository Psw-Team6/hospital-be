using IntegrationLibrary.BloodSubscription.Model;
using System.Collections.Generic;

namespace IntegrationAPI.Dtos.Request
{
    public class MounthlyBloodSubscriptionRequest
    {
        public string bloodBankName { get; set; }
        public List<AmountOfBloodType> bloodTypeAmountPair { get; set; }
    }
}
