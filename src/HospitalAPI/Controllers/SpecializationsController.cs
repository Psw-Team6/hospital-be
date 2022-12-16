using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpecializationsController:ControllerBase
    {
        private readonly SpecializationsService _specializationsService;
        private readonly IMapper _mapper;

        public SpecializationsController(SpecializationsService specializationsService, IMapper mapper)
        {
            _specializationsService = specializationsService;
            _mapper = mapper;
        }
        [HttpGet()]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SpecializationResponse>>> GetAll()
        {
            var specializations = await _specializationsService.GetAll();
            var result = _mapper.Map<IEnumerable<SpecializationResponse>>(specializations);
            return specializations == null ? NotFound() : Ok(result);
        }
        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecializationResponse>> Create(SpecializationRequest specializationDto)
        {
            var specialization = _mapper.Map<Specialization>(specializationDto);
            var specializationCreated = await _specializationsService.Create(specialization);
            var result = _mapper.Map<SpecializationResponse>(specializationCreated);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut]
        [ProducesResponseType( StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(SpecializationRequest specializationDto)
        {
            var specialization = _mapper.Map<Specialization>(specializationDto);
            var spec = await _specializationsService.Update(specialization);
            return spec ? NoContent() : NotFound();
        }
        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecializationResponse>> GetById(Guid id)
        {
            var specialization = await _specializationsService.GetById(id);
            var result = _mapper.Map<SpecializationResponse>(specialization);
            return specialization == null ? NotFound() : Ok(result);
        }

        [HttpGet("/getByName/{name}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecializationResponse>> GetByName(string name)
        {
            var specialization = await _specializationsService.GetByName(name);
            var result = _mapper.Map<SpecializationResponse>(specialization);
            return specialization == null ? NotFound() : Ok(result);
        }
    }
}