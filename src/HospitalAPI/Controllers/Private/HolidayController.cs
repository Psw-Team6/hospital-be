using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Holidays.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Private
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly HolidayService _holidayService;
        private readonly IMapper _mapper;

        public HolidayController(HolidayService holidayService, IMapper mapper)
        {
            _holidayService = holidayService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<HolidayResponse>>> GetAllHolidays()
        {
            var holidays = await _holidayService.GetAllHolidays();
            return Ok(holidays);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HolidayResponse>> ScheduleHoliday([FromBody] HolidayRequest holidayRequest)
        {
            var holiday = _mapper.Map<Holiday>(holidayRequest);
            var holidayScheduled = await _holidayService.ScheduleHoliday(holiday);
            var result = _mapper.Map<HolidayResponse>(holidayScheduled);
            return CreatedAtAction("ScheduleHoliday", new {id = result.Id}, result);
        }
        
        
        [HttpGet("GetDoctorHolidays/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<HolidayResponse>>> GetDoctorHolidays([FromRoute]Guid id)
        {
            var holidays = await _holidayService.GetDoctorsHolidays(id);
            var result = _mapper.Map<List<HolidayResponse>>(holidays);
            return result == null ? NotFound() : Ok(result);
        }

    }
}