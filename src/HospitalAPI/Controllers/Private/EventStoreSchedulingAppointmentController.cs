using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalLibrary.Appointments.DomainEvents;
using HospitalLibrary.Appointments.Service.EventStoreService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Private
{
    [Route("api/v1/[controller]")]
    [ApiController]
    /*
    [HospitalAuthorization(UserRole.Manager)]
    */
    public class EventStoreSchedulingAppointmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly EventStoreSchedulingAppointmentService _eventStoreSchedulingAppointmentService;

        public EventStoreSchedulingAppointmentController(IMapper mapper,
            EventStoreSchedulingAppointmentService eventStoreSchedulingAppointmentService)
        {
            _mapper = mapper;
            _eventStoreSchedulingAppointmentService = eventStoreSchedulingAppointmentService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetAverageStepCount()
        {
            var result = await _eventStoreSchedulingAppointmentService.GetAverageStepCount();
            return Ok(result);
        } 
        
        [HttpGet("get-time")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TimeSpan>> GetAverageTime()
        {
            var result = await _eventStoreSchedulingAppointmentService.GetAverageTime();
            return Ok(result);
        }

        [HttpGet("get-average-count-type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Dictionary<EventStoreSchedulingAppointmentType, int>>> GetAverageCountAllTypes()
        {
            var result = await _eventStoreSchedulingAppointmentService.GetAverageCountForEveryStep();
            return Ok(result);
        }
        [HttpGet("get-average-time-type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Dictionary<EventStoreSchedulingAppointmentService, double>>> GetAverageTimeForEveryStep()
        {
            var result = await _eventStoreSchedulingAppointmentService.GetAverageTimeForEveryStep();
            return Ok(result);
        }
    }
}