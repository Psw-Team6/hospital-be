using IntegrationLibrary.BloodRequests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.HTTP
{
    public interface IHttpService
    {
        Task<BloodSupplyResponse> GetProductAsync(string path);
    }
}
