namespace HospitalAPI.Dtos.Request
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PortalUrl { get; set; }
    }
}