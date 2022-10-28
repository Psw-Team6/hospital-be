using System;
using System.Net;
using System.Threading.Tasks;
using HospitalLibrary.CustomException;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HospitalAPI.Exceptions
{
    public class ExceptionMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DoctorException e)
            {
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(e.Message);
            }
        }
        

        
    }
}