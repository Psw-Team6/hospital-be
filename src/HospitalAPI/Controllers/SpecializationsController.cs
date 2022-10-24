using System;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
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

        public SpecializationsController(SpecializationsService specializationsService)
        {
            _specializationsService = specializationsService;
        }
        [HttpPost]
        public async Task<ActionResult> Create(SpecializationDto specialization)
        {
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }
            var spec = await _specializationsService.Create(specialization);
            return CreatedAtAction(nameof(GetById), new { id = spec.Id }, spec);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var specialization = await _specializationsService.GetById(id);
            return specialization == null ? NotFound() : Ok(specialization);
        }
    }
}