using IntegrationLibrary.BloodStatistic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodStatistic.Service
{
    public interface IBloodStatisticService
    {
        public List<BloodStatisticResponse> getTenderStatistic(DateRange range);

    }
}
