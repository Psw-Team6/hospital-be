using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController( DoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<DoctorResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DoctorResponse>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAll();
            var result = _mapper.Map<List<DoctorResponse>>(doctors);
           return Ok(result);
        }
        [HttpGet("username/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DoctorResponse>> GetByUsername([FromRoute]string username)
        {
            var doctor =  await _doctorService.GetByUsername(username);
            var result = _mapper.Map<DoctorResponse>(doctor);
            return result == null ? NotFound() : Ok(result);
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DoctorResponse>> CreateDoctor([FromBody] DoctorRequest doctorRequest)
        {
            var doctor = _mapper.Map<Doctor>(doctorRequest);
            var result = await _doctorService.CreateDoctor(doctor);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DoctorResponse>> GetById([FromRoute]Guid id)
        {
           var doctor =  await _doctorService.GetById(id);
           var result = _mapper.Map<DoctorResponse>(doctor);
           return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById( Guid id)
        {
            var result = await _doctorService.DeleteById(id);
            return result ? NoContent() : NotFound();
        }
    }
}