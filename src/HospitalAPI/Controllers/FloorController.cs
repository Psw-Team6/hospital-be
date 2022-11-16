using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
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
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(FloorRequest floorDto)
        {
            var floor = _mapper.Map<Floor>(floorDto);
            var res = await _floorService.Update(floor);
            return res ? NoContent() : NotFound();
        }
    }
}