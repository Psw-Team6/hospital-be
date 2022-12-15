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