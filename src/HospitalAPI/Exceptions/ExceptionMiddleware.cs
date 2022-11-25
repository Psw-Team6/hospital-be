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
                await BadRequestException(context, error);
            }
            catch (DoctorException error)
            {
                await BadRequestException(context, error);
            }
            catch (PatientException e)
            {
                await BadRequestException(context, e);
            }
            catch (AuthenticationException e)
            {
                await BadRequestException(context, e);
            }
            catch (PatientAdmissionException e)
            {
                await NotFoundException(context, e);
            }
            catch (PatientDischargeException e)
            {
                await BadRequestException(context, e);
            }
            catch (TreatmentReportException e)
            {
                await NotFoundException(context, e);
            }
        }

        private static async Task BadRequestException(HttpContext context, Exception e)
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
        private static async Task NotFoundException(HttpContext context, Exception e)
        {
            var response = context.Response;
            //Set response ContentType
            response.ContentType = "application/json";
            response.StatusCode = (int) HttpStatusCode.NotFound;
            var responseContent = new ResponseContent()
            {
                Error = e.Message
            };
            var jsonResult = JsonConvert.SerializeObject(responseContent);
            await context.Response.WriteAsync(jsonResult);
        }
    }
}