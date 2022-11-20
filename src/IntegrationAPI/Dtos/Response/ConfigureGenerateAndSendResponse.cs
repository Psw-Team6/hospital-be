namespace IntegrationAPI.Dtos.Response
{
    public class ConfigureGenerateAndSendResponse
    {
        public bool Response { get; set; }
        public int StatusCode { get; set; }

        public ConfigureGenerateAndSendResponse(bool response, int statusCode)
        {
            Response = response;
            StatusCode = statusCode;
        }
    }
}
