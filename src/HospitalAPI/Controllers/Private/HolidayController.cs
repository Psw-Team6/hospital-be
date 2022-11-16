using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        public async Task<ActionResult<List<Holiday>>> GetAllHolidays()
        {
            var holidays = await _holidayService.GetAllHolidays();
            return Ok(holidays);
        }

    }
}