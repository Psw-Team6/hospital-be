using IntegrationLibrary.BloodRequests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.HTTP
{
    public class HttpService : IHttpService
    {

        public static HttpClient client = new HttpClient();


        public async Task<BloodSupplyResponse> GetProductAsync(string path)
        {
            BloodSupplyResponse bloodSupplyResponse = new BloodSupplyResponse(false, 0);

            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                bloodSupplyResponse.StatusCode = (int)response.StatusCode;
                bloodSupplyResponse.Response = Boolean.Parse(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException httpEx)
            {
                if (httpEx.StatusCode.HasValue)
                {
                    bloodSupplyResponse.StatusCode = (int)httpEx.StatusCode;
                }
                else
                {
                    if (httpEx.Message.Contains("No connection could be made because the target machine actively refused it."))
                        bloodSupplyResponse.StatusCode = 500;

                }

            }
            return bloodSupplyResponse;
        }
    }
}
