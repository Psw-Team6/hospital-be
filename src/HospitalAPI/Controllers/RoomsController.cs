using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalAPI.Infrastructure.Authorization;
using HospitalLibrary.ApplicationUsers.Model;
using Microsoft.AspNetCore.Http;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HospitalAuthorization(UserRole.Manager)]
    public class RoomsController : ControllerBase
    {
        private readonly RoomService _roomService;
        private readonly IMapper _mapper;

        public RoomsController(RoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        
        // GET: api/rooms
        [HttpGet]
        [ProducesResponseType(typeof(List<RoomResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RoomResponse>>> GetAll()
        {
            var rooms = await _roomService.GetAll();
            var result = _mapper.Map<List<RoomResponse>>(rooms);
            return Ok(result);
        }
        
        // GET: api/rooms
        [HttpGet("{buildingId}/{floorId}")]
        [ProducesResponseType(typeof(List<RoomResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RoomResponse>>> GetAllByBuildingAndFloor([FromRoute]Guid buildingId, [FromRoute]Guid floorId)
        {
            var rooms = await _roomService.GetAllByBuildingIdAndFloorId(buildingId,floorId);
            var result = _mapper.Map<List<RoomResponse>>(rooms);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomResponse>> GetById(Guid id)
        {
            var room = await _roomService.GetById(id);
            var result = _mapper.Map<RoomResponse>(room);
            return room == null ? NotFound() : Ok(result);

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] RoomRequest roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            var res = await _roomService.Update(room);
            return res ? NoContent() : NotFound();
        }
    }
}
