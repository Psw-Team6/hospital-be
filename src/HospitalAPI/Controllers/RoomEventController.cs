using System;
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
    public class RoomEventController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IRoomEventService _roomEventService;


        public RoomEventController(IRoomEventService roomEventService, IMapper mapper)
        {
            _roomEventService = roomEventService;
            _mapper = mapper;
        }

        [HttpPost("createEvent")]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomEventResponse>> CreateEvent([FromBody] RoomEventRequest roomEventDto)
        {
            var roomEvent = _mapper.Map<RoomEvent>(roomEventDto);
            var roomEventCreated = await _roomEventService.Create(roomEvent);
            
            if (roomEventCreated == null)
            {
                return BadRequest();
            }
            
            return Ok(roomEventCreated);
        }

    }
}