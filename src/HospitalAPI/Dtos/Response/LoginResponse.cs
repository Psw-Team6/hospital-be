using HospitalLibrary.ApplicationUsers.Model;

namespace HospitalAPI.Dtos.Response
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        //public UserToken UserToken { get; set; }
    }
}