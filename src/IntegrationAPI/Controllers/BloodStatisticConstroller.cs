
using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using IntegrationLibrary.HTTP;
using IntegrationLibrary.BloodRequests.Model;
using IntegrationAPI.Dtos.Response;
using IntegrationLibrary.BloodStatistic.Model;
using IntegrationLibrary.BloodStatistic.Service;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodStatisticController : ControllerBase
    {
        private readonly IBloodStatisticService bloodStatisticService;
        private static readonly HttpClient client = new HttpClient();

        public BloodStatisticController(IBloodStatisticService bloodStatisticService)
        {
            this.bloodStatisticService = bloodStatisticService;
        }

        [HttpPut("getTenderStatistic")]
        public List<BloodStatisticResponse> getTenderStatistic(DateRange range)
        {
            return bloodStatisticService.getTenderStatistic(range);
        }

        [HttpPut("getUrgentStatistic")]
        public List<BloodStatisticResponse> getUrgentStatistic(DateRange range)
        {
            List<BloodUnit> units = getUrgentUnits().Result.Value;
            return bloodStatisticService.getUrgentStatistic(range, units);
        }


        [HttpGet("getUrgentUnits")]
        public async Task<ActionResult<List<BloodUnit>>> getUrgentUnits()
        {
            try
            {
                var result = client.GetFromJsonAsync<List<BloodUnit>>("http://localhost:5000/api/v1/BloodUnit/getUrgentUnits").Result;
                return result;
            }
            catch (HttpRequestException httpEx)
            {
                if (httpEx.StatusCode.HasValue)
                {
                    var p = (int)httpEx.StatusCode;
                }
                else
                {
                    return BadRequest();
                }

            }

            return null;
        }
    }
}
