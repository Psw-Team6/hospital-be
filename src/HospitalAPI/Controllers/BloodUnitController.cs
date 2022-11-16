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
            var units = await _bloodUnitService.GetUnits();
            return Ok(units);
        }
        
        [HttpGet("GetAllBloodUnits")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BloodUnit>>> GetAllBloodUnits()
        {
            var units = await _bloodUnitService.GetAllBloodUnits();
            return Ok(units);
        }
        
        [HttpPost("getAvailableBloodUnit")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BloodUnit>> GetAvailableBloodUnit([FromBody] AvailableBloodUnitRequest request)
        {
            
            var result = await _bloodUnitService.GetAvailableBloodUnit(request.BloodType, request.Amount);
            return result == null ? NotFound() : Ok(result);
        }
        
    }
}