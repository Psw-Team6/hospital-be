using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Patients.Service;
using HospitalLibrary.Rooms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BuildingController: ControllerBase
    {
        private readonly BuildingService _buildingService;
        private readonly IMapper _mapper;
        
        public BuildingController(BuildingService buildingService, IMapper mapper)
        {
            _buildingService = buildingService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BuildingResponse>>> GetAllBuildings()
        {
            var buildings = await _buildingService.GetAll();
            var result = _mapper.Map<List<BuildingResponse>>(buildings);
            return Ok(result);
        }
        
    }
}