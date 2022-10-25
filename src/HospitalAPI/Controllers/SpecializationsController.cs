using System;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Service;
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
        [HttpPost]
        public async Task<ActionResult> Create(SpecializationRequest specializationDto)
        {
            var specialization = _mapper.Map<Specialization>(specializationDto);
            var specializationCreated = await _specializationsService.Create(specialization);
            var result = _mapper.Map<SpecializationResponse>(specializationCreated);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut]
        public async Task<ActionResult> Update(SpecializationRequest specializationDto)
        {
            var specialization = _mapper.Map<Specialization>(specializationDto);
            var spec = await _specializationsService.Update(specialization);
            return spec ? NoContent() : NotFound();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var specialization = await _specializationsService.GetById(id);
            var result = _mapper.Map<SpecializationResponse>(specialization);
            return specialization == null ? NotFound() : Ok(result);
        }

        [HttpGet("/getByName/{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
            var specialization = await _specializationsService.GetByName(name);
            var result = _mapper.Map<SpecializationResponse>(specialization);
            return specialization == null ? NotFound() : Ok(result);
        }
    }
}