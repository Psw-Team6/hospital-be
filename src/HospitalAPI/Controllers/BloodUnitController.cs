using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.BloodUnits.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IntegrationLibrary.Tender.Model;
using Newtonsoft.Json;
using HospitalLibrary.SharedModel;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BloodUnitController : ControllerBase
    {
        private readonly BloodUnitService _bloodUnitService;
        private readonly IMapper _mapper;
        private static readonly HttpClient client = new HttpClient();

        public BloodUnitController(BloodUnitService bloodUnitService, IMapper mapper)
        {
            _bloodUnitService = bloodUnitService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BloodUnitDto>>> GetUnits()
        {
            var units = await _bloodUnitService.GetUnitsGroupByType();
            return Ok(units);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] BloodUnit bu)
        {
            var result = await _bloodUnitService.Update(bu);
            return result == false ? NotFound() : NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BloodUnit>> CreateBloodUnit([FromBody] BloodUnit bu)
        {
            var result = await _bloodUnitService.Create(bu);
            return CreatedAtAction(nameof(CreateBloodUnit), new { id = result.Id }, result);
        }
        
        [HttpGet("getClosedTenders")]
        public async Task<ActionResult<List<Tender>>> getClosedTenders()
        {
           
            try
            {
                HttpResponseMessage response =  await client.GetAsync("http://localhost:5001/api/Tender/getClosedTenders");
                var r = await response.Content.ReadAsStringAsync();
                List<Tender> result = JsonConvert.DeserializeObject<List<Tender>>(r);
                return result;
            }
            catch (HttpRequestException httpEx)
            {
                if (httpEx.StatusCode.HasValue)
                {
                    var p =  (int)httpEx.StatusCode;
                }
                else
                {
                    return BadRequest();
                }

            }

            return null;
        }

        [HttpGet("getUrgentUnits")]
        public async Task<ActionResult<List<BloodUnitForStatisticDto>>> getUrgentUnits()
        {
            var units = await _bloodUnitService.GetUrgentUnits();
            return Ok(units);
        }
    }
}