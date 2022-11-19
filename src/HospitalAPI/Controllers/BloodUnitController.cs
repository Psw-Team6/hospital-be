using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.BloodUnits.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BloodUnitController : ControllerBase
    {
        private readonly BloodUnitService _bloodUnitService;
        private readonly IMapper _mapper;

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
        
    }
}