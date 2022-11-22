using System.Linq;
using HospitalAPI.Extensions;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalAPI.Infrastructure.Authorization
{
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly UserRole _role;

        public AuthorizeActionFilter(UserRole role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var dbContext = context.HttpContext.RequestServices.GetRequiredService<HospitalDbContext>();
            var role = context.HttpContext.User.GetUserRole();
            //var user = dbContext.ApplicationUsers.SingleOrDefault(x => x.Username == username);
            if (role != _role.ToString())
            {
                context.Result = new ForbidResult();
            }
        }
    }
}