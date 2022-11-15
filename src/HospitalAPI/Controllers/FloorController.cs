using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Rooms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FloorController: ControllerBase
    {
        private readonly FloorService _floorService;
        private readonly IMapper _mapper;
        
        public FloorController(FloorService floorService, IMapper mapper)
        {
            _floorService = floorService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<FloorResponse>>> GetAllFloors()
        {
            var floors = await _floorService.GetAll();
            var result = _mapper.Map<List<FloorResponse>>(floors);
            return Ok(result);
        }
        
    }
}