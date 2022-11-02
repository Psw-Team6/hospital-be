namespace IntegrationAPI.Dtos.Response
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
