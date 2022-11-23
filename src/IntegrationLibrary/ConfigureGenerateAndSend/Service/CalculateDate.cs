using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.ConfigureGenerateAndSend.Service
{
   public  class CalculateDate
    {
        public CalculateDate()
        {
        }

        public int DefinePeriodForSendingReports(string date)
        {
            int days = 0;
            if (date.Equals("ONE_MONTH"))
                days = 30;
            else if (date.Equals("TWO_MONTH"))
                days = 61;
            else if (date.Equals("THREE_MONTH"))
                days = 91;
            else if (date.Equals("SIX_MONTH"))
                days = 183;
            else if (date.Equals("ONE_YEAR"))
                days = 365;
            else if (date.Equals("EVERY_TWO_MINUT"))
                days = 0;
            else
            {
                days = CalculateNumberOfDays(DateTime.Parse(date));
            }

            return days;
        }


        public int CalculateNumberOfDays(DateTime date)
        {
            return (int)Math.Round((date - DateTime.Now).TotalDays);
        }


    }
}
