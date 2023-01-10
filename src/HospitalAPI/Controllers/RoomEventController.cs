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
            Console.WriteLine("POGODJEN ZA EVENJTE");
            var roomEvent = _mapper.Map<RoomEvent>(roomEventDto);
            var roomEventCreated = await _roomEventService.Create(roomEvent);
            
            if (roomEventCreated == null)
            {
                return BadRequest();
            }
            
            return Ok(roomEventCreated);
        }

        [HttpGet("/api/v1/Merging-succesful")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetSuccesfullMergingCount()
        {
            var mergingCount = await _roomEventService.SuccesfullMergingCount();
            return Ok(mergingCount);
        }

        [HttpGet("/api/v1/Spliting-succesful")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetSuccesfullSplitingCount()
        {
            var splitingCount = await _roomEventService.SuccesfullSplitingCount();
            return Ok(splitingCount);
        }

        [HttpGet("/api/v1/Merging-step-count")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int[]>> GetMergingStepCount()
        {
            var mergingStepCount = await _roomEventService.StepMergingCount();
            return Ok(mergingStepCount);
        }

        [HttpGet("/api/v1/Spliting-step-count")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int[]>> GetSplitingStepCount()
        {
            var splitingStepCount = await _roomEventService.StepSplitingCount();
            return Ok(splitingStepCount);
        }
        
        
        [HttpGet("/api/v1/Scheduling-cancel-count")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetSchedulingCancelCount()
        {
            var Count = await _roomEventService.SchedulingCanceledCount();
            return Ok(Count);
        }
        
        [HttpGet("/api/v1/GetEventsInLastDay")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RoomEvent>>> GetEventsInLastDay()
        {
            var events = await _roomEventService.GetRoomEventsInLastDay();
            return Ok(events);
        }
        
        [HttpGet("/api/v1/Average-merging-scheduling-time")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int[]>> GetAverageMergingSchedulingTime()
        {
            var splitingStepCount = await _roomEventService.GetAverageMergningSchedulingTimes();
            return Ok(splitingStepCount);
        }
        
        [HttpGet("/api/v1/Average-merging-step-time")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int[]>> GetAverageMergingStepTime()
        {
            var splitingStepCount = await _roomEventService.GetAverageMergningStepTimes();
            return Ok(splitingStepCount);
        }
        
        [HttpGet("/api/v1/Average-spliting-scheduling-time")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int[]>> GetAverageSplitingSchedulingTime()
        {
            var splitingStepCount = await _roomEventService.GetAverageSplitingSchedulingTimes();
            return Ok(splitingStepCount);
        }
        
        [HttpGet("/api/v1/Average-spliting-step-time")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int[]>> GetAverageSplitingStepTime()
        {
            var splitingStepCount = await _roomEventService.GetAverageSplitingStepTimes();
            return Ok(splitingStepCount);
        }
    }
}