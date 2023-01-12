using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodStatistic.Model
{
    public class BloodStatisticRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatisticSource Source { get; set; }
    }
}
