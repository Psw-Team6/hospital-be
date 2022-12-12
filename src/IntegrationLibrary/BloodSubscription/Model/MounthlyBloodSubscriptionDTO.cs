using IntegrationLibrary.BloodSubscription.Model;
using System.Collections.Generic;

namespace IntegrationLibrary.BloodSubscription.Model
{
    public class MounthlyBloodSubscriptionDTO
    {
        public string APIKey { get; set; }
        public List<AmountOfBloodType> bloodTypeAmountPair { get; set; }
        public string messageForManager { get; set; }
    }
}
