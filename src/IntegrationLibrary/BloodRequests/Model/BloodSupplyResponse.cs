using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodRequests.Model
{
    public class BloodSupplyResponse
    {
        public bool Response { get; set; }
        public int StatusCode { get; set; }

        public BloodSupplyResponse(bool response, int statusCode)
        {
            Response = response;
            StatusCode = statusCode;
        }
    }
}
