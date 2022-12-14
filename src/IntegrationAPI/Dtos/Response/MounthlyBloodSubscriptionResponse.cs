using IntegrationLibrary.BloodSubscription.Model;
using System.Collections.Generic;

namespace IntegrationAPI.Dtos.Response
{
    public class MounthlyBloodSubscriptionResponse
    {
        public string messageForManager { get; set; }
        public string APIKey { get; set; }
        public List<AmountOfBloodType> amountsOfBloodTypes { get; set; }
    }
}
