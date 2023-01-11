
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

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodStatisticController : ControllerBase
    {
        private readonly IBloodStatisticService bloodStatisticService;

        public BloodStatisticController(IBloodStatisticService bloodStatisticService)
        {
            this.bloodStatisticService = bloodStatisticService;

        }

        [HttpPut]
        public List<BloodStatisticResponse> getTenderStatistic(DateRange range)
        {
            return bloodStatisticService.getTenderStatistic(range);
        }
    }
}
