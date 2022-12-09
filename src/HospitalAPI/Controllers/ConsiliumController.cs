using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Consiliums.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConsiliumController: ControllerBase
    {
        private readonly ConsiliumService _consiliumService;
        private readonly IMapper _mapper;

        public ConsiliumController(ConsiliumService consiliumService, IMapper mapper)
        {
            _consiliumService = consiliumService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorResponse>>> GetAllConsiliums()
        {
            var consiliums = await _consiliumService.GetAll();
            var result = _mapper.Map<IEnumerable<ConsiliumResponse>>(consiliums);
            return result == null ? NotFound() : Ok(result);
        } 
        [HttpPost]
        public async Task<ActionResult<DoctorResponse>> ScheduleConsilium([FromBody] Consilium consiliumRequest)
        {

            var consilium = _mapper.Map<Consilium>(consiliumRequest);
            var newConsilium = await _consiliumService.ScheduleConsilium(consilium);
            var result = _mapper.Map<ConsiliumResponse>(newConsilium);
            return result == null ? NotFound() : Ok(result);
        }
    }
}