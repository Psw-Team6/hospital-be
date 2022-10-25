using System;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos;
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
        public async Task<ActionResult> Create(SpecializationDto specializationDto)
        {
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }
            var specialization = _mapper.Map<Specialization>(specializationDto);
            var spec = await _specializationsService.Create(specialization);
            var result = _mapper.Map<SpecializationDto>(spec);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var specialization = await _specializationsService.GetById(id);
            var result = _mapper.Map<SpecializationDto>(specialization);
            return specialization == null ? NotFound() : Ok(result);
        }
    }
}