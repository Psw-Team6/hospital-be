using System;
using HospitalLibrary.ApplicationUsers.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Infrastructure.Authorization
{
    public class HospitalAuthorizationAttribute : TypeFilterAttribute
    {
        public HospitalAuthorizationAttribute(UserRole role) : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { role };
        }
    }
}