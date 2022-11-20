using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.ScheduleTask.Service
{
    public interface IReportSenderService
    {
        public void IsTimeForSending();
        public List<DateTime> GetAllDateForSend();
    }
}
