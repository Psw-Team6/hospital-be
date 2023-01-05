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
        
        [HttpGet("getRoomMergingById/{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomMergingResponse>> GetMergingById(Guid id)
        {
            var roomMerging = await _roomRenovationService.GetMergingById(id);
            var result = _mapper.Map<RoomMergingResponse>(roomMerging);
            return roomMerging == null ? NotFound() : Ok(result);
        }
        
        [HttpGet("getRoomSplitingById/{id}")]
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
        
        
        [HttpGet("GetAllSplittingByRoomId/{roomId}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomSplitingResponse>> GetAllSplittingByRoomId([FromRoute]Guid roomId)
        {
            var result = await _roomRenovationService.GetAllSplittingByRoomId(roomId);
            return result == null ? NotFound() : Ok(result);
           
        }
        [HttpDelete ("DeleteSplitting/{id}")] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteById([FromRoute]Guid id)
        {
            var result = await _roomRenovationService.GetSplitingById(id);
            if (result == null)
                return NotFound();
            
            if(await _roomRenovationService.DeleteSplitting(result) == false)
                return BadRequest();
            
            return NoContent();
           
        }

        [HttpGet("GetAllMergingByRoomId/{VroomId}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomMerging>> GetAllMergingByRoomId([FromRoute]Guid VroomId)
        {
            var result = await _roomRenovationService.GetAllMergingByRoomId(VroomId);
            return result == null ? NotFound() : Ok(result);
           
        }
        
        [HttpDelete ("DeleteMerging/{id}")] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromRoute]Guid id)
        {
            var result = await _roomRenovationService.GetMergingById(id);
            if (result == null)
                return NotFound();
            
            if(await _roomRenovationService.DeleteMerging(result) == false)
                return BadRequest();
            
            return NoContent();
           
        }
        
        
        
    }
}