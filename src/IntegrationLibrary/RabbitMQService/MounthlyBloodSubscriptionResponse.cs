using IntegrationLibrary.BloodSubscription.Model;
using System;
using System.Collections.Generic;

namespace IntegrationLibrary.RabbitMQPublisher
{
    public class MounthlyBloodSubscriptionResponse
    {
        public string APIKey { get; set; }
        public List<AmountOfBloodType> bloodTypeAmountPair { get; set; }
        public DateTime dateAndTimeOfSubscription { get; set; }
    }
}
