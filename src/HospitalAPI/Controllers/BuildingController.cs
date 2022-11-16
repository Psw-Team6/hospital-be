using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
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
            var result = _mapper.Map<IEnumerable<BuildingResponse>>(buildings);
            return Ok(result);
        }
        
        
        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomResponse>> GetById(Guid id)
        {
            var room = await _buildingService.GetById(id);
            var result = _mapper.Map<BuildingResponse>(room);
            return room == null ? NotFound() : Ok(result);

        }
        
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(BuildingRequest buildingDto)
        {
            var building = _mapper.Map<Building>(buildingDto);
            var res = await _buildingService.Update(building);
            return res == false ? NotFound() : NoContent();
        }
    }
}