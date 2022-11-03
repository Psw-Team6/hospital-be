using System;
using System.Net;
using System.Threading.Tasks;
using HospitalLibrary.CustomException;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
            catch (DateRangeException error)
            {
                var response = context.Response;
                //Set response ContentType
                response.ContentType = "application/json";
                response.StatusCode = (int) HttpStatusCode.BadRequest;
                var responseContent = new ResponseContent()
                {
                    Error = error.Message
                };
                var jsonResult = JsonConvert.SerializeObject(responseContent);
                await context.Response.WriteAsync(jsonResult);
            }
            catch (DoctorException error)
            {
                var response = context.Response;
                //Set response ContentType
                response.ContentType = "application/json";
                response.StatusCode = (int) HttpStatusCode.BadRequest;
                var responseContent = new ResponseContent()
                {
                    Error = error.Message
                };
                var jsonResult = JsonConvert.SerializeObject(responseContent);
                await context.Response.WriteAsync(jsonResult);
            }
            catch (PatientException e)
            {
                var response = context.Response;
                //Set response ContentType
                response.ContentType = "application/json";
                response.StatusCode = (int) HttpStatusCode.BadRequest;
                var responseContent = new ResponseContent()
                {
                    Error = e.Message
                };
                var jsonResult = JsonConvert.SerializeObject(responseContent);
                await context.Response.WriteAsync(jsonResult);
            }
        }
        

        
    }
}