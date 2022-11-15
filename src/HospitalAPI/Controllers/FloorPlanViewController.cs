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
    public class FloorPlanViewController: ControllerBase
    {
        private readonly FloorPlanViewService _floorPlanViewService;
        private readonly IMapper _mapper;
        
        public FloorPlanViewController(FloorPlanViewService floorPlanViewService, IMapper mapper)
        {
            _floorPlanViewService = floorPlanViewService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<FloorPlanViewResponse>>> GetAllFloors()
        {
            var floorsPlanViews = await _floorPlanViewService.GetAll();
            var result = _mapper.Map<List<FloorPlanViewResponse>>(floorsPlanViews);
            return Ok(result);
        }
    }
}