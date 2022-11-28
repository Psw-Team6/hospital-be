using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodSubscription.Model
{
    public class MounthlyBloodSubscription
    {
        public Guid id { get; set; }
        public Guid bloodBankId { get; set; }
        public DateTime dateAndTimeOfSubscription { get; set; }
        public List<AmountOfBloodType> amountOfBloodTypes { get; set; }
    }
}
