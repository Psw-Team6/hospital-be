﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using HospitalAPI.Validations.ErrorCodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HospitalAPI.Validations.Filter
{
    public class ValidationFilter: IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
                var errorResponse = new ErrorResponse();
                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ErrorCode
                        {
                            FieldName = error.Key,
                            Message = subError
                        };
                        errorResponse.Errors.Add(errorModel);
                    }
                    
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }
            await next();
        }
    }
}