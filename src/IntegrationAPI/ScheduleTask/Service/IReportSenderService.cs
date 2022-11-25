using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAPI.ScheduleTask.Service
{
    public interface IReportSenderService
    {
        public void TimeForSending();
        public List<DateTime> GetAllDateForSend();
    }
}
