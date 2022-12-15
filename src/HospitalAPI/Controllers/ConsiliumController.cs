using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Consiliums.Service;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConsiliumController: ControllerBase
    {
        private readonly ConsiliumService _consiliumService;
        private readonly SpecializationsService _specializationsService;
        private readonly IMapper _mapper;

        public ConsiliumController(ConsiliumService consiliumService, IMapper mapper, SpecializationsService specializationsService)
        {
            _consiliumService = consiliumService;
            _mapper = mapper;
            _specializationsService = specializationsService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorResponse>>> GetAllConsiliums()
        {
            var consiliums = await _consiliumService.GetAll();
            var result = _mapper.Map<IEnumerable<ConsiliumResponse>>(consiliums);
            return result == null ? NotFound() : Ok(result);
        } 
        [HttpPost]
        [ProducesResponseType(typeof(ConsiliumResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConsiliumResponse>> ScheduleConsilium([FromBody] ConsiliumRequest consiliumRequest)
        {
            var consilium = _mapper.Map<Consilium>(consiliumRequest);
            var newConsilium = await _consiliumService.ScheduleConsilium(consilium);
            var result = _mapper.Map<ConsiliumResponse>(newConsilium);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }
        
        [HttpPost("specialization")]
        [ProducesResponseType(typeof(ConsiliumResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConsiliumResponse>> ScheduleConsiliumSpecialization([FromBody] ConsiliumSpecializationRequest consiliumRequest)
        {
            var spec = _mapper.Map<IEnumerable<Specialization>>(consiliumRequest.Specializations);
            var specializations = await _specializationsService.GetSpecializations(spec);
            var consilium = _mapper.Map<Consilium>(consiliumRequest);
            var newConsilium = await _consiliumService.ScheduleConsiliumSpecialization(consilium,specializations,consiliumRequest.DoctorId);
            var result = _mapper.Map<ConsiliumResponse>(newConsilium);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ConsiliumResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsiliumResponse>> GetById([FromRoute] Guid id)
        {
            var consilium = await _consiliumService.GetById(id);
            var result = _mapper.Map<ConsiliumResponse>(consilium);
            return result == null ? NotFound() : Ok(result);
        }
        [HttpGet("doctor/{id}")]
        public async Task<ActionResult<IEnumerable<ConsiliumResponse>>> GetConsiliumsForDoctor([FromRoute] Guid id)
        {
            var consiliums = await _consiliumService.GetConsiliumsForDoctor(id);
            var result = _mapper.Map<IEnumerable<ConsiliumResponse>>(consiliums);
            return result == null ? NotFound() : Ok(result);
        }
    }
}