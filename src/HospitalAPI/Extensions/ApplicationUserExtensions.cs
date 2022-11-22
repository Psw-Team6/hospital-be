using System.Linq;
using System.Security.Claims;

namespace HospitalAPI.Extensions
{
    public static class ApplicationUserExtensions
    {
        public static string GetUserRole(this ClaimsPrincipal user)
        {
          return   user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }
              
    }
}