using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace HospitalAPI.Extentsions
{
    public static class ApplicationUserExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
          return   user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }
              
    }
}