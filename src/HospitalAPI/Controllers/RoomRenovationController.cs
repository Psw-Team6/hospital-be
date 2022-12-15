using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[HospitalAuthorization(UserRole.Manager)]
    public class RoomRenovationController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private IRoomRenovationService _roomRenovationService;
        
        public RoomRenovationController(IRoomRenovationService roomRenovationService, IMapper mapper)
        {
            _roomRenovationService = roomRenovationService;
            _mapper = mapper;
        }
        
        [HttpPost("createMerging")]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomMergingResponse>> CreateMerging([FromBody] RoomMergingResponse roomMergingDto)
        {
            var roomMerging = _mapper.Map<RoomMerging>(roomMergingDto);
            var roomMergingCreated = await _roomRenovationService.CreateRoomMerging(roomMerging);
            
            var result = _mapper.Map<RoomMergingResponse>(roomMergingCreated);
            if (result == null)
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(GetMergingById), new { id = result.Id }, result);
        }
        
        [HttpPost("createSpliting")]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomSplitingResponse>> CreateSpliting([FromBody] RoomSplitingResponse roomSplitingDto)
        {
            var roomSpliting = _mapper.Map<RoomSpliting>(roomSplitingDto);
            var roomSplitingCreated = await _roomRenovationService.CreateRoomSpliting(roomSpliting);
            
            var result = _mapper.Map<RoomSplitingResponse>(roomSplitingCreated);
            if (result == null)
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(GetSplitingById), new { id = result.Id }, result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomMergingResponse>> GetMergingById(Guid id)
        {
            var roomMerging = await _roomRenovationService.GetMergingById(id);
            var result = _mapper.Map<RoomMergingResponse>(roomMerging);
            return roomMerging == null ? NotFound() : Ok(result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomSplitingResponse>> GetSplitingById(Guid id)
        {
            var roomSpliting = await _roomRenovationService.GetSplitingById(id);
            var result = _mapper.Map<RoomSplitingResponse>(roomSpliting);
            return roomSpliting == null ? NotFound() : Ok(result);
        }
        
        [HttpPost("getAvailableMerging")]
        [ProducesResponseType(typeof(List<RoomMergingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RoomMergingResponse >>> GetAllAvailableAppointmentsForRoomMerging([FromBody]RoomMergingRequest roomMergingAppointmentRequest)
        {
            var appointmentRequested = _mapper.Map<RoomMerging>(roomMergingAppointmentRequest);
    
            var appointments = await _roomRenovationService.GetAllAvailableAppointmentsForRoomMerging(appointmentRequested);
    
            var result = _mapper.Map<List<RoomMergingResponse >>(appointments);
            return result == null ? BadRequest() : Ok(result);
        }
        
        
        [HttpPost("getAvailableSpliting")]
        [ProducesResponseType(typeof(List<RoomSplitingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RoomSplitingResponse >>> GetAllAvailableAppointmentsForRoomSpliting([FromBody]RoomSplitingRequest roomSplitingAppointmentRequest)
        {
            var appointmentRequested = _mapper.Map<RoomSpliting>(roomSplitingAppointmentRequest);
    
            var appointments = await _roomRenovationService.GetAllAvailableAppointmentsForRoomSpliting(appointmentRequested);
    
            var result = _mapper.Map<List<RoomSplitingResponse >>(appointments);
            return result == null ? BadRequest() : Ok(result);
        }
    }
}