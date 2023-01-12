using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Examinations.Service.EventStoreService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Private
{
    [Route("api/v1/[controller]")]
    [ApiController]
    /*
    [HospitalAuthorization(UserRole.Manager)]
    */
    public class EventStoreExaminationController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly EventStoreExaminationService _eventStoreExaminationService;

        public EventStoreExaminationController(IMapper mapper, EventStoreExaminationService eventStoreExaminationService)
        {
            _mapper = mapper;
            _eventStoreExaminationService = eventStoreExaminationService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetAverageStepCount()
        {
            var result = await _eventStoreExaminationService.GetAverageStepCount();
            return Ok(result);
        } 
        [HttpGet("get-time")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TimeSpan>> GetAverageTime()
        {
            var result = await _eventStoreExaminationService.GetAverageTime();
            return Ok(result);
        }

        [HttpGet("get-average-count-type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Dictionary<EventStoreExaminationType, int>>> GetAverageCountAllTypes()
        {
            var result = await _eventStoreExaminationService.GetAverageCountForEveryStep();
            return Ok(result);
        }
        [HttpGet("get-average-time-type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Dictionary<EventStoreExaminationType, double>>> GetAverageTimeForEveryStep()
        {
            var result = await _eventStoreExaminationService.GetAverageTimeForEveryStep();
            return Ok(result);
        }
    }
}